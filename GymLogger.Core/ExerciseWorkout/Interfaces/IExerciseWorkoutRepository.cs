using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Core.ExerciseWorkout.Interfaces;
public interface IExerciseWorkoutRepository
{
    IPagedResult<IExerciseWorkoutDetail> GetPagedAsync(IExerciseWorkoutPagedRequest request);
    Task<IExerciseWorkout?> GetLatestForCurrentUserAsync(Guid exerciseId, Guid? workoutId);
}
