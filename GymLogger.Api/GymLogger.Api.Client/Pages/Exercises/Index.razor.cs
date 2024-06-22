using GymLogger.Api.Client.Components;
using GymLogger.Api.Client.Components.Dialog;
using GymLogger.Api.Client.Pages.Exercises.Components;
using GymLogger.Common.Enums;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Exercises;

public partial class Index : BaseComponent
{
    [Inject] public required IExerciseApiService ExerciseHttpService { get; set; }
    [Inject] public required IMuscleGroupApiService MuscleGroupHttpService { get; set; }
    [Inject] public required IDialogService DialogService { get; set; }

    private ICollection<ExerciseDto> Items { get; set; } = [];
    private ICollection<MuscleGroupDto> MuscleGroups { get; set; } = [];
    private string? _searchValue;
    private ExercisePagedRequestDto _pagedRequestDto = new() { SortColumn = "Name" };
    private PaginationState _paginationState = new() { ItemsPerPage = 12 };
    private PagingDataResponseDto _pagingDataResponseDto = new();
    private bool sortMenuOpen = false;
    private bool filterOpen = false;

    private string SelectedExerciseLogType
    {
        get => _pagedRequestDto.ExerciseLogType.ToString();
        set => _pagedRequestDto.ExerciseLogType = Enum.Parse<ExerciseLogType>(value);
    }

    private string SelectedMuscleGroupId
    {
        get => _pagedRequestDto.MuscleGroupId.ToString();
        set => _pagedRequestDto.MuscleGroupId = string.IsNullOrEmpty(value) ? Guid.Empty : Guid.Parse(value);
    }

    protected override async Task OnInitializedAsync()
    {
        this.MuscleGroups.Add(new MuscleGroupDto(Guid.Empty, "Select muscle group", ""));
        var muscleGroups = await MuscleGroupHttpService.GetAllAsync();
        foreach (var muscleGroup in muscleGroups)
        {
            this.MuscleGroups.Add(muscleGroup);
        }
        Console.WriteLine("MuscleGroups: " + this.MuscleGroups.Count());
        await this.LoadDataAsync();
        await _paginationState.SetCurrentPageIndexAsync(0);
    }

    private async Task OnSearchAsync()
    {
        this._pagedRequestDto.Search = _searchValue;
        this._pagedRequestDto.Page = 0;
        await this.LoadDataAsync();
    }

    private async Task HandlePageChangedAsync(int newPage)
    {
        await this._paginationState.SetCurrentPageIndexAsync(newPage);
        this._pagedRequestDto.Page = newPage;
        await this.LoadDataAsync();
    }

    private async Task HandlePageSizeChangeAsync(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var newPageSize))
        {
            this._pagedRequestDto.Page = 0;
            this._pagedRequestDto.PageSize = newPageSize;

            this._paginationState.ItemsPerPage = newPageSize;
            await _paginationState.SetCurrentPageIndexAsync(0);

            await this.LoadDataAsync();
        }
    }

    private async Task LoadDataAsync()
    {
        var results = await ExerciseHttpService.GetPagedListAsync(this._pagedRequestDto);
        this._pagingDataResponseDto = results.PagingData;
        this.Items = results.Items;
        await _paginationState.SetTotalItemCountAsync(this._pagingDataResponseDto.TotalItems);
    }

    private async Task CreateAsync()
    {
        var dto = new ExerciseCreateDto();
        var dialog = await DialogService.ShowDialogAsync<ExerciseFormDialog>(dto, new DialogParameters()
        {
            Title = $"Nova vježba",
            PreventDismissOnOverlayClick = true,
            PreventScroll = true,
        });

        var result = await dialog.Result;
        if (!result.Cancelled && result.Data != null)
        {
            await ExerciseHttpService.CreateAsync((ExerciseCreateDto)result.Data);
            await this.LoadDataAsync();
        }
    }

    private async Task EditAsync(ExerciseDto dto)
    {
        var createDto = new ExerciseCreateDto()
        {
            Description = dto.Description,
            Name = dto.Name,
            MuscleGroupId = dto.MuscleGroupId,
            ExerciseLogType = dto.ExerciseLogType,
            IsPublic = dto.IsPublic
        };

        var dialog = await DialogService.ShowDialogAsync<ExerciseFormDialog>(createDto, new DialogParameters()
        {
            Title = $"Nova vježba",
            PreventDismissOnOverlayClick = true,
            PreventScroll = true,
        });

        var result = await dialog.Result;
        if (!result.Cancelled && result.Data != null)
        {
            var editDto = new ExerciseUpdateDto()
            {
                Id = dto.Id,
                Description = ((ExerciseCreateDto)result.Data).Description,
                Name = ((ExerciseCreateDto)result.Data).Name,
                MuscleGroupId = ((ExerciseCreateDto)result.Data).MuscleGroupId,
                ExerciseLogType = ((ExerciseCreateDto)result.Data).ExerciseLogType,
                IsPublic = ((ExerciseCreateDto)result.Data).IsPublic
            };
            await ExerciseHttpService.UpdateAsync(editDto);
            await this.LoadDataAsync();
        }
    }

    private async Task DeleteAsync(Guid id)
    {
        var dialog = await DialogService.ShowDialogAsync<DeleteDialog>("Želite li stvarno obrisati ovu vježbu?", new DialogParameters()
        {
            Title = $"Brisanje vježbe",
            PreventDismissOnOverlayClick = true,
            PreventScroll = true,
        });

        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            await ExerciseHttpService.DeleteAsync(id);
            await this.LoadDataAsync();
        }
    }

    private void OnMenuChange(MenuChangeEventArgs args)
    {
        if (args.Value != null)
        {
            Console.WriteLine(args.Id);
        }
    }

    private async Task OnSortMenuItemClicked(string sortColumn, Common.Enums.SortDirection sortDirection)
    {
        this._pagedRequestDto.SortColumn = sortColumn;
        this._pagedRequestDto.SortDirection = sortDirection;
        this.sortMenuOpen = false;
        await this.LoadDataAsync();
    }

    private async Task FilterAsync()
    {
        await this.LoadDataAsync();
    }

    private async Task ClearFilterAsync()
    {
        this.SelectedExerciseLogType = ExerciseLogType.Unknown.ToString();
        this.SelectedMuscleGroupId = Guid.Empty.ToString();
    }
}
