namespace GymLogger.Core.ExerciseWorkout.Interfaces;
public interface IExerciseWorkoutRepository
{
    Task<IExerciseWorkout?> GetLatestForCurrentUserAsync(Guid exerciseId, Guid? workoutId);
}
