using GymLogger.Shared.Models.ExerciseWorkout;
using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Workouts.Components;

public partial class ExerciseWorkoutItem
{
    [Parameter] public bool ShowName { get; set; } = true;
    [Parameter] public ExerciseWorkoutDto Exercise { get; set; }
}
