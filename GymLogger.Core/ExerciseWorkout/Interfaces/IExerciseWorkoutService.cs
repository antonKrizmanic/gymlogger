namespace GymLogger.Core.ExerciseWorkout.Interfaces;
public interface IExerciseWorkoutService
{
    Task<IExerciseWorkout?> GetLatestForCurrentUserAsync(Guid exerciseId, Guid? workoutId);
}
