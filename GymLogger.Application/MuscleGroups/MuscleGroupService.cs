using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Application.MuscleGroups;
internal class MuscleGroupService(IMuscleGroupsRepository repository) : IMuscleGroupService
{
    public IPagedResult<IMuscleGroup> GetPagedAsync(IPagedRequest request) => 
        repository.GetPagedAsync(request);

    public Task<IMuscleGroup?> GetByIdAsync(Guid id) =>
        repository.GetByIdAsync(id);
}
