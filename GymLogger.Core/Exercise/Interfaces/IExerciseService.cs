using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Core.Paging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Core.Exercise.Interfaces;
public interface IExerciseService
{
    IPagedResult<IExercise> GetPagedAsync(IPagedRequest request);
    Task<IExercise?> GetByIdAsync(Guid id);
}
