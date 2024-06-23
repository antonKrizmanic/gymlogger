using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Core.Workout.Interfaces;
public interface IWorkoutService
{
    IPagedResult<IWorkout> GetPagedAsync(IWorkoutPagedRequest request);
    Task<IWorkout?> GetByIdAsync(Guid id);
    Task<IWorkout> CreateAsync(IWorkoutCreate exercise);
    Task UpdateAsync(IWorkoutUpdate exercise);
    Task DeleteAsync(Guid id);
}
