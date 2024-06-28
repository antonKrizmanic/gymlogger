using GymLogger.Api.Client.Components;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Models.Workout;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Workouts;

public partial class Index : BaseComponent
{
    [Inject] public required IWorkoutApiService WorkoutApiService { get; set; }

    private PagedResponseDto<WorkoutDto> _pagedResponseDto = new();
    private WorkoutPagedRequestDto _pagedRequestDto = new() { SortColumn = "Date" };

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
