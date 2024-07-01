using GymLogger.Api.Client.Components;
using GymLogger.Api.Client.Components.Dialog;
using GymLogger.Api.Client.Models;
using GymLogger.Api.Client.Pages.Exercises.Components;
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

    private ICollection<MuscleGroupDto> MuscleGroups { get; set; } = [];
    private PagedResponseDto<ExerciseDto> _pagedResponseDto = new();
    public ExercisePagedRequestDto PagedRequestDto = new() { SortColumn = "Name" };

    private bool filterOpen = false;



    protected override async Task OnInitializedAsync()
    {
        this.MuscleGroups.Add(new MuscleGroupDto(Guid.Empty, "Select muscle group", ""));
        var muscleGroups = await MuscleGroupHttpService.GetAllAsync();
        foreach (var muscleGroup in muscleGroups)
        {
            this.MuscleGroups.Add(muscleGroup);
        }
        await this.LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        this._pagedResponseDto = await ExerciseHttpService.GetPagedListAsync(this.PagedRequestDto);
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
            try
            {
                await ExerciseHttpService.CreateAsync((ExerciseCreateDto)result.Data);
                await this.LoadDataAsync();
                this.ToastService.ShowSuccess("Vježba uspješno dodana.");
            }
            catch (Exception)
            {
                this.ToastService.ShowError("Dodavanje vježbe nije uspjelo.");
            }
        }
    }

    private void Details(ExerciseDto dto)
    {
        base.NavigationManager.NavigateTo($"/exercises/{dto.Id}");
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
            Title = $"Izmjena vježbe",
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
            try
            {
                await ExerciseHttpService.UpdateAsync(editDto);
                await this.LoadDataAsync();
                this.ToastService.ShowSuccess("Vježba uspješno izmjenjena.");
            }
            catch (Exception)
            {
                this.ToastService.ShowError("Izmjena vježbe nije uspjela.");
            }
        }
    }

    private async Task DeleteAsync(ExerciseDto item)
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
            try
            {
                await ExerciseHttpService.DeleteAsync(item.Id);
                await this.LoadDataAsync();
                this.ToastService.ShowSuccess("Vježba uspješno obrisana.");
            }
            catch (Exception)
            {
                this.ToastService.ShowError("Brisanje vježbe nije uspjelo.");
            }
        }
    }

    private async Task OnSortMenuItemClicked(SortEventArgs args)
    {
        this.PagedRequestDto.SortColumn = args.SortColumn;
        this.PagedRequestDto.SortDirection = args.SortDirection;
        await this.LoadDataAsync();
    }

    private async Task FilterAsync()
    {
        await this.LoadDataAsync();
    }
}
