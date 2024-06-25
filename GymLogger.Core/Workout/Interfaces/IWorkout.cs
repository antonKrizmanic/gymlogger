using GymLogger.Core.ExerciseWorkout.Interfaces;

namespace GymLogger.Core.Workout.Interfaces;
public interface IWorkout
{
    Guid Id { get; set; }
    string Name { get; set; }
    string? Description { get; set; }
    DateTime Date { get; set; }
    string? BelongsToUserId { get; set; }
    Guid MuscleGroupId { get; set; }
    decimal? TotalWeight { get; set; }
    decimal? TotalReps { get; set; }
    decimal? TotalSets { get; set; }
    ICollection<IExerciseWorkout> Exercises { get; set; }
}
