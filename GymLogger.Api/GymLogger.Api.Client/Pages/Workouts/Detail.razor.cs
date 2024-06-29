using GymLogger.Api.Client.Components;
using GymLogger.Shared.Models.Workout;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Workouts;

public partial class Detail : BaseComponent
{
    [Parameter] public Guid Id { get; set; }
    [Inject] private IWorkoutApiService WorkoutApiService { get; set; }
    private WorkoutDetailsDto Workout { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        this.Workout = await WorkoutApiService.GetAsync(Id);
    }
}