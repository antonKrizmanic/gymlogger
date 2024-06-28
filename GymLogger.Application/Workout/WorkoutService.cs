using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.Paging.Interfaces;
using GymLogger.Core.Workout.Interfaces;

namespace GymLogger.Application.Workout;
internal class WorkoutService(IWorkoutRepository repository, IExerciseRepository exerciseRepository) : IWorkoutService
{
    public IPagedResult<IWorkout> GetPagedAsync(IWorkoutPagedRequest request) =>
        repository.GetPaged(request);

    public Task<IWorkoutDetails?> GetByIdAsync(Guid id) =>
        repository.GetByIdAsync(id);

    public async Task<IWorkout> CreateAsync(IWorkoutCreate workout)
    {
        var exerciseIds = workout.Exercises.Select(x => x.ExerciseId).ToList();
        var exercises = await exerciseRepository.GetByIdsAsync(exerciseIds);

        var muscleGroupId = exercises
            .GroupBy(x => x.MuscleGroupId)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefault();

        foreach (var exercise in workout.Exercises)
        {
            exercise.TotalReps = exercise.Sets.Sum(x => x.Reps);
            exercise.TotalSets = exercise.Sets.Count;
            exercise.TotalWeight = exercise.Sets.Sum(x => x.Weight);
        }

        workout.MuscleGroupId = muscleGroupId;
        workout.TotalReps = workout.Exercises.Sum(x => x.TotalReps);
        workout.TotalSets = workout.Exercises.Sum(x => x.TotalSets);
        workout.TotalWeight = workout.Exercises.Sum(x => x.TotalWeight);

        return await repository.CreateAsync(workout);
    }

    public Task UpdateAsync(IWorkoutUpdate workout)
    {
        //TODO: Implement
        workout.MuscleGroupId = Guid.NewGuid();
        workout.TotalReps = 0;
        workout.TotalSets = 0;
        workout.TotalWeight = 0;

        return repository.UpdateAsync(workout);
    }

    public Task DeleteAsync(Guid id) =>
        repository.DeleteAsync(id);
}
