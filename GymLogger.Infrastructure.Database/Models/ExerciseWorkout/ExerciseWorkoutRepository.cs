using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymLogger.Application.User.Interfaces;
using GymLogger.Core.ExerciseWorkout.Interfaces;
using GymLogger.Core.Paging.Interfaces;
using GymLogger.Infrastructure.Database.Models.Paging;
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

    public IPagedResult<IExerciseWorkoutDetail> GetPagedAsync(IExerciseWorkoutPagedRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var query = dbContext.ExerciseWorkouts
            .Include(x => x.Exercise)
            .AsNoTracking()
            .AsQueryable();

        if (request.ExerciseId != Guid.Empty && request.ExerciseId != null)
        {
            query = query.Where(b => b.ExerciseId == request.ExerciseId);
        }

        if (request.WorkoutId != Guid.Empty && request.WorkoutId != null)
        {
            query = query.Where(b => b.WorkoutId == request.WorkoutId);
        }

        //var isSortDescending = request.SortDirection == SortDirection.Descending;

        //// Sort the results based on the sort column
        //query = request.SortColumn switch
        //{
        //    nameof(IExercise.Name) => isSortDescending
        //        ? query.OrderByDescending(b => b.Name)
        //        : query.OrderBy(b => b.Name),
        //    nameof(IExercise.MuscleGroupName) => isSortDescending
        //        ? query.OrderByDescending(b => b.MuscleGroup.Name)
        //        : query.OrderBy(b => b.MuscleGroup.Name),
        //    nameof(IExercise.ExerciseLogType) => isSortDescending
        //        ? query.OrderByDescending(b => b.ExerciseLogType)
        //        : query.OrderBy(b => b.ExerciseLogType),
        //    _ => isSortDescending
        //        ? query.OrderByDescending(b => b.CreatedAt)
        //    : query.OrderBy(b => b.CreatedAt)
        //};

        var projectQuery = query.ProjectTo<IExerciseWorkoutDetail>(mapper.ConfigurationProvider);

        return new PagedResult<IExerciseWorkoutDetail>(projectQuery);
    }
}
