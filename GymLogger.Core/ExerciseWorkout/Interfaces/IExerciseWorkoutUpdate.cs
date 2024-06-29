namespace GymLogger.Core.ExerciseWorkout.Interfaces;
public interface IExerciseWorkoutUpdate
{
    Guid WorkoutId { get; set; }
    Guid ExerciseId { get; set; }
    decimal? TotalWeight { get; set; }
    decimal? TotalReps { get; set; }
    decimal? TotalSets { get; set; }
}
