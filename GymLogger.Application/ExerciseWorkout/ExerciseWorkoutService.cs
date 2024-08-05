using GymLogger.Core.ExerciseWorkout.Interfaces;
using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Application.ExerciseWorkout;
internal class ExerciseWorkoutService(IExerciseWorkoutRepository repository) : IExerciseWorkoutService
{
    public async Task<IExerciseWorkout?> GetLatestForCurrentUserAsync(Guid exerciseId, Guid? workoutId) =>
        await repository.GetLatestForCurrentUserAsync(exerciseId, workoutId);

    public IPagedResult<IExerciseWorkoutDetail> GetPagedAsync(IExerciseWorkoutPagedRequest request) =>
        repository.GetPagedAsync(request);
}
