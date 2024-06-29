using GymLogger.Api.Client.Components;
using GymLogger.Api.Client.Components.Dialog;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Models.Workout;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Workouts;

public partial class Index : BaseComponent
{
    [Inject] public required IWorkoutApiService WorkoutApiService { get; set; }

    private PagedResponseDto<WorkoutDto> _pagedResponseDto = new();
    private WorkoutPagedRequestDto _pagedRequestDto = new() { SortColumn = "Date", SortDirection = Common.Enums.SortDirection.Descending };

    private bool sortMenuOpen = false;
    private bool filterOpen = false;

    protected override async Task OnInitializedAsync()
    {
        await this.LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        this._pagedResponseDto = await WorkoutApiService.GetPagedListAsync(this._pagedRequestDto);
    }

    private void Details(WorkoutDto dto)
    {
        base.NavigationManager.NavigateTo($"/workouts/{dto.Id}");
    }

    private void EditAsync(WorkoutDto dto)
    { }

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

    private async Task OnSortMenuItemClicked(string sortColumn, Common.Enums.SortDirection sortDirection)
    {
        this._pagedRequestDto.SortColumn = sortColumn;
        this._pagedRequestDto.SortDirection = sortDirection;
        this.sortMenuOpen = false;
        await this.LoadDataAsync();
    }
}
