using GymLogger.Api.Client.Components;
using GymLogger.Api.Client.Models.Workout;
using GymLogger.Shared.Models.ExerciseWorkout;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Workouts.Components;

public partial class ExerciseWorkoutFormItem : BaseComponent
{
    [Inject] private IExerciseWorkoutApiService ExerciseWorkoutApiService { get; set; }
    [Parameter] public Guid? WorkoutId { get; set; }
    [Parameter] public ExerciseSetCreateFormViewModel Item { get; set; }
    [Parameter] public EventCallback<Guid> OnRemoveExercise { get; set; }
    [Parameter] public EventCallback<EditSetEventArgs> OnRemoveSet { get; set; }
    [Parameter] public EventCallback<EditSetEventArgs> OnEditSet { get; set; }
    [Parameter] public EventCallback<EditSetEventArgs> OnCopySet { get; set; }
    [Parameter] public EventCallback<Guid> OnAddSet { get; set; }

    private ExerciseWorkoutDto? lastExerciseWorkout;

    protected override async Task OnInitializedAsync()
    {
        lastExerciseWorkout = await ExerciseWorkoutApiService.GetLatestForCurrentUserAsync(Item.Exercise.Id, WorkoutId);
        await base.OnInitializedAsync();
    }
}
