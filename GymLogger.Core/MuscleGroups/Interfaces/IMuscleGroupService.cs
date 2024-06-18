using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Core.MuscleGroups.Interfaces;
public interface IMuscleGroupService
{
    IPagedResult<IMuscleGroup> GetPagedAsync(IPagedRequest request);
    Task<IMuscleGroup?> GetByIdAsync(Guid id);

}
