using GymLogger.Shared.Models.ExerciseWorkout;

namespace GymLogger.Shared.Services;
public interface IExerciseWorkoutApiService
{
    Task<ExerciseWorkoutDto?> GetLatestForCurrentUserAsync(Guid exerciseId, Guid? workoutId);
}
