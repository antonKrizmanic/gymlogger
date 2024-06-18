using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Core.Exercise.Interfaces;

public interface IExerciseRepository
{
    IPagedResult<IExercise> GetPagedAsync(IPagedRequest request);
    Task<IExercise?> GetByIdAsync(Guid id);
}
