using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.Paging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Application.Exercise;
internal class ExerciseService(IExerciseRepository repository) : IExerciseService
{
    public Task<IExercise?> GetByIdAsync(Guid id) =>
        repository.GetByIdAsync(id);


    public IPagedResult<IExercise> GetPagedAsync(IPagedRequest request) =>
        repository.GetPagedAsync(request);
}
