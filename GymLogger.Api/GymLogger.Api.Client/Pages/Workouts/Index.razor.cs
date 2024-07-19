using GymLogger.Api.Client.Components;
using GymLogger.Api.Client.Components.Dialog;
using GymLogger.Api.Client.Models;
using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Models.Workout;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Workouts;

public partial class Index : BaseComponent
{
    [Inject] public required IWorkoutApiService WorkoutApiService { get; set; }
    [Inject] public required IMuscleGroupApiService MuscleGroupHttpService { get; set; }

    public WorkoutPagedRequestDto PagedRequestDto = new() { SortColumn = "Date", SortDirection = Common.Enums.SortDirection.Descending };
    private PagedResponseDto<WorkoutDto> _pagedResponseDto = new();
    private ICollection<MuscleGroupDto> MuscleGroups { get; set; } = [];

    private bool sortMenuOpen = false;
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
        this._pagedResponseDto = await WorkoutApiService.GetPagedListAsync(this.PagedRequestDto);
    }

    private void Details(WorkoutDto dto)
    {
        base.NavigationManager.NavigateTo($"/workouts/{dto.Id}");
    }

    private void EditAsync(WorkoutDto dto)
    {
        base.NavigationManager.NavigateTo($"/workouts/edit/{dto.Id}");
    }

    private async Task DeleteAsync(WorkoutDto dto)
    {
        var dialog = await DialogService.ShowDialogAsync<DeleteDialog>("Želite li stvarno obrisati ovaj trening?", new DialogParameters()
        {
            Title = $"Brisanje treninga",
            PreventDismissOnOverlayClick = true,
            PreventScroll = true,
        });

        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            try
            {
                await WorkoutApiService.DeleteAsync(dto.Id);
                await this.LoadDataAsync();
                base.ToastService.ShowSuccess("Trening uspješno obrisan.");
            }
            catch (Exception)
            {
                base.ToastService.ShowError("Trening nije obrisan");
                throw;
            }
        }
    }

    private async Task OnSortMenuItemClicked(SortEventArgs args)
    {
        this.PagedRequestDto.SortColumn = args.SortColumn;
        this.PagedRequestDto.SortDirection = args.SortDirection;
        this.sortMenuOpen = false;
        await this.LoadDataAsync();
    }
    
    private async Task FilterAsync()
    {
        await this.LoadDataAsync();
    }
}
