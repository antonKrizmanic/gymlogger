using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Core.Exercise.Interfaces;

public interface IExerciseRepository
{
    IPagedResult<IExercise> GetPaged(IExercisePagedRequest request);
    Task<IExercise?> GetByIdAsync(Guid id);
    Task<ICollection<IExercise>> GetByIdsAsync(ICollection<Guid> ids);
    Task<IExercise> CreateAsync(IExerciseCreate exercise);
    Task UpdateAsync(IExerciseUpdate exercise);
    Task DeleteAsync(Guid id);
}
