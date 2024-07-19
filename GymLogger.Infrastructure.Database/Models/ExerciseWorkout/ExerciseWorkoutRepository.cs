using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymLogger.Application.User.Interfaces;
using GymLogger.Core.ExerciseWorkout.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymLogger.Infrastructure.Database.Models.ExerciseWorkout;
internal class ExerciseWorkoutRepository(GymLoggerDbContext dbContext, ICurrentUserProvider currentUserProvider, IMapper mapper, ILogger<ExerciseWorkoutRepository> logger) : IExerciseWorkoutRepository
{
    public async Task<IExerciseWorkout?> GetLatestForCurrentUserAsync(Guid exerciseId, Guid? workoutId = null)
    {
        var exerciseWorkout = await dbContext.ExerciseWorkouts
            .AsNoTracking()
            .Where(x => x.ExerciseId == exerciseId &&
                x.WorkoutId != workoutId &&
                x.BelongsTo == currentUserProvider.GetCurrentUserId())
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<IExerciseWorkout>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return exerciseWorkout;
    }
}
