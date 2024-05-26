using GymLogger.Core.Paging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Core.MuscleGroups.Interfaces;
public interface IMuscleGroupsRepository
{
    IPagedResult<IMuscleGroup> GetPagedAsync(IPagedRequest request);
    Task<IMuscleGroup?> GetByIdAsync(Guid id);
}
