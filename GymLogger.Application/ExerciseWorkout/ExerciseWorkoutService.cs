using GymLogger.Core.ExerciseWorkout.Interfaces;

namespace GymLogger.Application.ExerciseWorkout;
internal class ExerciseWorkoutService(IExerciseWorkoutRepository repository) : IExerciseWorkoutService
{
    public async Task<IExerciseWorkout?> GetLatestForCurrentUserAsync(Guid exerciseId, Guid? workoutId) =>
        await repository.GetLatestForCurrentUserAsync(exerciseId, workoutId);
}
