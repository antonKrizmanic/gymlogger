using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymLogger.Application.User.Interfaces;
using GymLogger.Common.Enums;
using GymLogger.Core.CodeExtensions;
using GymLogger.Core.Paging.Interfaces;
using GymLogger.Core.Workout.Interfaces;
using GymLogger.Exceptions;
using GymLogger.Infrastructure.Database.Models.ExerciseSet;
using GymLogger.Infrastructure.Database.Models.ExerciseWorkout;
using GymLogger.Infrastructure.Database.Models.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymLogger.Infrastructure.Database.Models.Workout;
internal class WorkoutRepository(GymLoggerDbContext dbContext, ICurrentUserProvider currentUserProvider, IMapper mapper, ILogger<WorkoutRepository> logger) : IWorkoutRepository
{
    public IPagedResult<IWorkout> GetPaged(IWorkoutPagedRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var query = dbContext.Workouts
            .AsNoTracking()
            .AsQueryable();

        if (request.Search != null)
        {
            query = query.Where(b => b.Name.Contains(request.Search));
        }

        var isSortDescending = request.SortDirection == SortDirection.Descending;

        // Sort the results based on the sort column
        query = request.SortColumn?.ToLower() switch
        {
            "name" => isSortDescending
                ? query.OrderByDescending(b => b.Name)
                : query.OrderBy(b => b.Name),
            "muscleGroupName" => isSortDescending
                ? query.OrderByDescending(b => b.MuscleGroup.Name)
                : query.OrderBy(b => b.MuscleGroup.Name),
            "date" => isSortDescending
                ? query.OrderByDescending(b => b.Date)
                : query.OrderBy(b => b.Date),
            _ => isSortDescending
                ? query.OrderByDescending(b => b.Date)
            : query.OrderBy(b => b.Date)
        };

        var projectQuery = query.ProjectTo<IWorkout>(mapper.ConfigurationProvider);

        return new PagedResult<IWorkout>(projectQuery);
    }

    public async Task<IWorkoutDetails?> GetByIdAsync(Guid id)
    {
        return await dbContext.Workouts
            .AsNoTracking()
            .ProjectTo<IWorkoutDetails>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IWorkout> CreateAsync(IWorkoutCreate workout)
    {
        ArgumentNullException.ThrowIfNull(workout);

        // TODO: Workouts and sets
        var dbEntity = new DbWorkout
        {
            Name = workout.Name,
            Description = workout.Description,
            Date = workout.Date,
            MuscleGroupId = workout.MuscleGroupId,
            TotalWeight = workout.TotalWeight,
            TotalReps = workout.TotalReps,
            TotalSets = workout.TotalSets,
            BelongsToUserId = currentUserProvider.GetCurrentUserId(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        if (workout.Exercises != null)
        {
            foreach (var exercise in workout.Exercises)
            {
                var dbExercise = new DbExerciseWorkout
                {
                    ExerciseId = exercise.ExerciseId,
                    TotalReps = exercise.TotalReps,
                    TotalSets = exercise.TotalSets,
                    TotalWeight = exercise.TotalWeight,
                    Sets = exercise.Sets.Select(x => new DbExerciseSet
                    {
                        Reps = x.Reps,
                        Weight = x.Weight,
                        Time = x.Time,
                        Index = x.Index,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }).ToList(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                dbEntity.Exercises = dbEntity.Exercises ?? new List<DbExerciseWorkout>();
                dbEntity.Exercises.Add(dbExercise);
            }
        }

        try
        {
            dbContext.Workouts.Add(dbEntity);
            await dbContext.SaveChangesAsync();

            logger.LogInformation($"Created Exercise with id {dbEntity.Id}");
            return dbEntity.MapTo<IWorkout>(mapper);
        }
        catch (Exception ex)
        {
            throw new GymLoggerException($"Failed to create exercise {dbEntity.Name}", ex);
        }
    }

    public async Task UpdateAsync(IWorkoutUpdate workout)
    {
        ArgumentNullException.ThrowIfNull(workout);

        var dbEntity = await dbContext.Workouts.FindAsync(workout.Id);

        if (dbEntity == null)
        {
            throw new GymLoggerEntityNotFoundException($"No Exercise was found for id {workout.Id}");
        }

        try
        {
            // TODO: Workouts and sets
            dbEntity.Name = workout.Name;
            dbEntity.Description = workout.Description;
            dbEntity.MuscleGroupId = workout.MuscleGroupId;
            dbEntity.Date = workout.Date;
            dbEntity.UpdatedAt = DateTime.Now;

            await dbContext.SaveChangesAsync();

            logger.LogInformation($"Updated Workout with id {workout.Id}");
        }
        catch (Exception ex)
        {
            throw new GymLoggerException($"Failed to update Workout {dbEntity.Name}", ex);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var dbEntity = await dbContext.Workouts
            .Include(x => x.Exercises)
            .ThenInclude(x => x.Sets)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (dbEntity == null)
        {
            throw new GymLoggerEntityNotFoundException($"No Workout was found for id {id}");
        }

        try
        {
            dbContext.Workouts.Remove(dbEntity);
            await dbContext.SaveChangesAsync();

            logger.LogInformation($"Deleted Workout with id {id}");
        }
        catch (Exception ex)
        {
            throw new GymLoggerException($"Failed to delete Workout {dbEntity.Name}", ex);
        }
    }
}
