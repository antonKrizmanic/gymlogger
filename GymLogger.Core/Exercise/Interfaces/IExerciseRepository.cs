using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Core.Exercise.Interfaces;

public interface IExerciseRepository
{
    IPagedResult<IExercise> GetPagedAsync(IExercisePagedRequest request);
    Task<IExercise?> GetByIdAsync(Guid id);
    Task<IExercise> CreateAsync(IExerciseCreate exercise);
    Task UpdateAsync(IExerciseUpdate exercise);
    Task DeleteAsync(Guid id);
}
