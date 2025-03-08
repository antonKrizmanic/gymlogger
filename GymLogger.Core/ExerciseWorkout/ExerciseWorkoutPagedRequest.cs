using GymLogger.Common.Enums;
using GymLogger.Core.ExerciseWorkout.Interfaces;

namespace GymLogger.Core.ExerciseWorkout;
public class ExerciseWorkoutPagedRequest : IExerciseWorkoutPagedRequest
{
    public Guid? ExerciseId { get; set; }
    public Guid? WorkoutId { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Search { get; set; }
    public string? SortColumn { get; set; }
    public SortDirection SortDirection { get; set; }
}
