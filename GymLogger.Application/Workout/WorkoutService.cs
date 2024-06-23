using GymLogger.Core.Paging.Interfaces;
using GymLogger.Core.Workout.Interfaces;

namespace GymLogger.Application.Workout;
internal class WorkoutService(IWorkoutRepository repository) : IWorkoutService
{
    public IPagedResult<IWorkout> GetPagedAsync(IWorkoutPagedRequest request) =>
        repository.GetPaged(request);

    public Task<IWorkout?> GetByIdAsync(Guid id) =>
        repository.GetByIdAsync(id);

    public Task<IWorkout> CreateAsync(IWorkoutCreate exercise)
    {
        //TODO: Implement
        exercise.MuscleGroupId = Guid.NewGuid();
        exercise.TotalReps = 0;
        exercise.TotalSets = 0;
        exercise.TotalWeight = 0;

        return repository.CreateAsync(exercise);
    }

    public Task UpdateAsync(IWorkoutUpdate exercise)
    {
        //TODO: Implement
        exercise.MuscleGroupId = Guid.NewGuid();
        exercise.TotalReps = 0;
        exercise.TotalSets = 0;
        exercise.TotalWeight = 0;

        return repository.UpdateAsync(exercise);
    }

    public Task DeleteAsync(Guid id) =>
        repository.DeleteAsync(id);
}
