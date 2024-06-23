using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Application.Exercise;
internal class ExerciseService(IExerciseRepository repository) : IExerciseService
{
    public Task<IExercise?> GetByIdAsync(Guid id) =>
        repository.GetByIdAsync(id);

    public IPagedResult<IExercise> GetPagedAsync(IExercisePagedRequest request) =>
        repository.GetPaged(request);

    public Task<IExercise> CreateAsync(IExerciseCreate exercise) =>
        repository.CreateAsync(exercise);

    public Task UpdateAsync(IExerciseUpdate exercise) =>
        repository.UpdateAsync(exercise);

    public Task DeleteAsync(Guid id) =>
        repository.DeleteAsync(id);
}
