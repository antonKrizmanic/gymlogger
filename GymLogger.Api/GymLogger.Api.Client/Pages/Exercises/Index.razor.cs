using GymLogger.Api.Client.Components;
using GymLogger.Api.Client.Components.Dialog;
using GymLogger.Api.Client.Pages.Exercises.Components;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Exercises;

public partial class Index : BaseComponent
{
    [Inject] private IExerciseApiService ExerciseHttpService { get; set; }
    [Inject] private IDialogService DialogService { get; set; }

    private ICollection<ExerciseDto> items = new List<ExerciseDto>();
    private string? _searchValue;
    private PagedRequestDto _pagedRequestDto = new() { SortColumn = "Name" };
    private PaginationState _paginationState = new() { ItemsPerPage = 10 };
    private PagingDataResponseDto _pagingDataResponseDto = new();

    protected override async Task OnInitializedAsync()
    {
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
        this.items = results.Items;
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
            ExerciseLogType = dto.ExerciseLogType
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
                ExerciseLogType = ((ExerciseCreateDto)result.Data).ExerciseLogType
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
}
