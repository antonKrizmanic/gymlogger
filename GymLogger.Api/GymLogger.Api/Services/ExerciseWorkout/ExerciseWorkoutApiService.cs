using AutoMapper;
using GymLogger.Core.CodeExtensions;
using GymLogger.Core.ExerciseWorkout.Interfaces;
using GymLogger.Shared.Models.ExerciseWorkout;
using GymLogger.Shared.Services;

namespace GymLogger.Api.Services.ExerciseWorkout;

internal class ExerciseWorkoutApiService(IExerciseWorkoutService service, IMapper mapper) : IExerciseWorkoutApiService
{
    public async Task<ExerciseWorkoutDto?> GetLatestForCurrentUser(Guid exerciseId, Guid? workoutId)
    {
        var entity = await service.GetLatestForCurrentUserAsync(exerciseId, workoutId);

        if (entity == null)
        {
            return null;
        }

        return entity.MapTo<ExerciseWorkoutDto>(mapper);
    }
}
