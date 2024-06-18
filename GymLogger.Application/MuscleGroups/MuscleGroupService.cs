using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Core.Paging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Application.MuscleGroups;
public class MuscleGroupService(IMuscleGroupsRepository repository) : IMuscleGroupService
{
    public IPagedResult<IMuscleGroup> GetPagedAsync(IPagedRequest request) => 
        repository.GetPagedAsync(request);

    public Task<IMuscleGroup?> GetByIdAsync(Guid id) =>
        repository.GetByIdAsync(id);
}
