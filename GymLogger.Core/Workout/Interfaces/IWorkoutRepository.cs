using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Core.Workout.Interfaces;
public interface IWorkoutRepository
{
    IPagedResult<IWorkout> GetPaged(IWorkoutPagedRequest request);
    Task<IWorkout?> GetByIdAsync(Guid id);
    Task<IWorkout> CreateAsync(IWorkoutCreate workout);
    Task UpdateAsync(IWorkoutUpdate workout);
    Task DeleteAsync(Guid id);
}
