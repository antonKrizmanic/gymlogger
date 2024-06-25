namespace GymLogger.Core.ExerciseWorkout.Interfaces;

public interface IExerciseWorkout
{
    Guid ExerciseId { get; set; }
    Guid WorkoutId { get; set; }
    decimal? TotalWeight { get; set; }
    decimal? TotalReps { get; set; }
    decimal? TotalSets { get; set; }
}
