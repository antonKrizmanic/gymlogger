using GymLogger.Core.ExerciseWorkout.Interfaces;

namespace GymLogger.Application.ExerciseWorkout;
internal class ExerciseWorkoutService(IExerciseWorkoutRepository repository) : IExerciseWorkoutService
{
    public Task<IExerciseWorkout?> GetLatestForCurrentUserAsync(Guid exerciseId, Guid? workoutId) =>
        repository.GetLatestForCurrentUserAsync(exerciseId, workoutId);
}
