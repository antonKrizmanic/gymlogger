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
        query = request.SortColumn switch
        {
            nameof(IWorkout.Name) => isSortDescending
                ? query.OrderByDescending(b => b.Name)
                : query.OrderBy(b => b.Name),
            nameof(IWorkout.MuscleGroupName) => isSortDescending
                ? query.OrderByDescending(b => b.MuscleGroup.Name)
                : query.OrderBy(b => b.MuscleGroup.Name),
            nameof(IWorkout.Date) => isSortDescending
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

        var dbEntity = new DbWorkout
        {
            Name = workout.Name,
            Description = workout.Description,
            Date = workout.Date.ToUniversalTime(),
            MuscleGroupId = workout.MuscleGroupId,
            TotalWeight = workout.TotalWeight,
            TotalReps = workout.TotalReps,
            TotalSets = workout.TotalSets,
            BelongsToUserId = currentUserProvider.GetCurrentUserId(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        if (workout.Exercises != null)
        {
            foreach (var exercise in workout.Exercises)
            {
                var dbExercise = new DbExerciseWorkout
                {
                    ExerciseId = exercise.ExerciseId,
                    Index = exercise.Index,
                    TotalReps = exercise.TotalReps,
                    TotalSets = exercise.TotalSets,
                    TotalWeight = exercise.TotalWeight,
                    Note = exercise.Note,
                    Sets = exercise.Sets.Select(x => new DbExerciseSet
                    {
                        Reps = x.Reps,
                        Weight = x.Weight,
                        Time = x.Time,
                        Index = x.Index,
                        Note = x.Note,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }).ToList(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
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

        // Load workout with exercises and sets
        var dbEntity = await dbContext.Workouts
            .Include(x => x.Exercises!)
            .ThenInclude(x => x.Sets!)
            .FirstOrDefaultAsync(x => x.Id == workout.Id);

        if (dbEntity == null)
        {
            throw new GymLoggerEntityNotFoundException($"No Exercise was found for id {workout.Id}");
        }

        try
        {
            dbEntity.Name = workout.Name;
            dbEntity.Description = workout.Description;
            dbEntity.MuscleGroupId = workout.MuscleGroupId;
            dbEntity.Date = workout.Date.ToUniversalTime();
            dbEntity.TotalWeight = workout.TotalWeight;
            dbEntity.TotalReps = workout.TotalReps;
            dbEntity.TotalSets = workout.TotalSets;
            dbEntity.UpdatedAt = DateTime.Now.ToUniversalTime();

            // Handle exercises. Add new, update existing, remove deleted
            var existingExerciseIds = dbEntity.Exercises.Select(e => e.ExerciseId).ToList();
            var updatedExerciseIds = workout.Exercises.Select(e => e.ExerciseId).ToList();

            // Remove deleted exercises
            var exercisesToRemove = dbEntity.Exercises.Where(e => !updatedExerciseIds.Contains(e.ExerciseId)).ToList();
            foreach (var exercise in exercisesToRemove)
            {
                dbContext.ExerciseWorkouts.Remove(exercise);
            }

            // Add new exercises
            var exercisesToAdd = workout.Exercises.Where(e => !existingExerciseIds.Contains(e.ExerciseId)).ToList();
            foreach (var exercise in exercisesToAdd)
            {
                var dbExercise = new DbExerciseWorkout
                {
                    ExerciseId = exercise.ExerciseId,
                    Index = exercise.Index,
                    TotalReps = exercise.TotalReps,
                    TotalSets = exercise.TotalSets,
                    TotalWeight = exercise.TotalWeight,
                    Note = exercise.Note,
                    Sets = exercise.Sets.Select(x => new DbExerciseSet
                    {
                        Reps = x.Reps,
                        Weight = x.Weight,
                        Time = x.Time,
                        Index = x.Index,
                        Note = x.Note,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }).ToList(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                dbEntity.Exercises.Add(dbExercise);
            }

            // Update existing exercises
            var exercisesToUpdate = workout.Exercises.Where(e => existingExerciseIds.Contains(e.ExerciseId)).ToList();
            foreach (var exercise in exercisesToUpdate)
            {
                var dbExercise = dbEntity.Exercises.First(e => e.ExerciseId == exercise.ExerciseId);
                dbExercise.Index = exercise.Index;
                dbExercise.TotalReps = exercise.TotalReps;
                dbExercise.TotalSets = exercise.TotalSets;
                dbExercise.TotalWeight = exercise.TotalWeight;
                dbExercise.Note = exercise.Note;
                dbExercise.UpdatedAt = DateTime.UtcNow;

                // Remove all existing sets
                dbContext.ExerciseSets.RemoveRange(dbExercise.Sets);

                // Add new sets
                dbExercise.Sets = exercise.Sets.Select(x => new DbExerciseSet
                {
                    Reps = x.Reps,
                    Weight = x.Weight,
                    Time = x.Time,
                    Index = x.Index,
                    Note = x.Note,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }).ToList();
            }

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
            .Include(x => x.Exercises!)
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
