using GymLogger.Shared.Models.ExerciseWorkout;

namespace GymLogger.Shared.Services;
public interface IExerciseWorkoutApiService
{
    Task<ExerciseWorkoutDto?> GetLatestForCurrentUser(Guid exerciseId, Guid? workoutId);
}
