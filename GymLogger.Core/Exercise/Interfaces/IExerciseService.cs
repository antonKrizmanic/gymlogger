using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Core.Exercise.Interfaces;
public interface IExerciseService
{
    IPagedResult<IExercise> GetPagedAsync(IPagedRequest request);
    Task<IExercise?> GetByIdAsync(Guid id);
    Task<IExercise> CreateAsync(IExerciseCreate exercise);
    Task UpdateAsync(IExerciseUpdate exercise);
    Task DeleteAsync(Guid id);
}
