using GymLogger.Shared.Models.ExerciseWorkout;
using GymLogger.Shared.Services.Generics;

namespace GymLogger.Shared.Services;
public interface IExerciseWorkoutApiService :
    IPagedHttpService<ExerciseWorkoutDetailDto, ExerciseWorkoutPagedRequestDto>
{
    Task<ExerciseWorkoutDto?> GetLatestForCurrentUserAsync(Guid exerciseId, Guid? workoutId);
}
