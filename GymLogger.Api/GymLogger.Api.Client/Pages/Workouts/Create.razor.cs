using GymLogger.Api.Client.Components;
using GymLogger.Shared.Models.Workout;
using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Workouts;

public partial class Create : BaseComponent
{
    [Inject] private NavigationManager NavigationManager { get; set; }
    public WorkoutCreateDto Model { get; set; } = new WorkoutCreateDto();
    private async Task SaveAsync()
    {

    }

    private void Cancel()
    {
        this.NavigationManager.NavigateTo("/workouts");
    }
}
