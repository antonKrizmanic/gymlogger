using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymLogger.Application.User.Interfaces;
using GymLogger.Core.ExerciseWorkout.Interfaces;
using GymLogger.Infrastructure.Database.Models.Workout;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymLogger.Infrastructure.Database.Models.ExerciseWorkout;
internal class ExerciseWorkoutRepository(GymLoggerDbContext dbContext, ICurrentUserProvider currentUserProvider, IMapper mapper, ILogger<ExerciseWorkoutRepository> logger) : IExerciseWorkoutRepository
{
    public async Task<IExerciseWorkout?> GetLatestForCurrentUserAsync(Guid exerciseId, Guid? workoutId = null)
    {
        var workout = new DbWorkout();
        if (workoutId != null && workoutId.Value != Guid.Empty)
        {
            workout = await dbContext.Workouts
                .AsNoTracking()
                .Where(x => x.Id == workoutId.Value)
                .FirstOrDefaultAsync();
        }
        else
        {
            workout = null;
        }

        var exerciseWorkout = dbContext.ExerciseWorkouts
            .AsNoTracking()
            .Where(x => x.ExerciseId == exerciseId &&
                x.WorkoutId != workoutId)
            .AsQueryable();

        if (workout != null)
        {
            exerciseWorkout = exerciseWorkout
                .Where(x => x.Workout.Date <= workout.Date);
        }

        return await exerciseWorkout
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<IExerciseWorkout>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}
