using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Core.MuscleGroups.Interfaces;
public interface IMuscleGroupsRepository
{
    IPagedResult<IMuscleGroup> GetPagedAsync(IPagedRequest request);
    Task<IMuscleGroup?> GetByIdAsync(Guid id);
}
