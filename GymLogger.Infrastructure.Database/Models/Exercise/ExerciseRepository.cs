using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymLogger.Common.Enums;
using GymLogger.Core.CodeExtensions;
using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.Paging.Interfaces;
using GymLogger.Exceptions;
using GymLogger.Infrastructure.Database.Models.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymLogger.Infrastructure.Database.Models.Exercise;
internal class ExerciseRepository(GymLoggerDbContext dbContext, IMapper mapper, ILogger<ExerciseRepository> logger) : IExerciseRepository
{
    public Task<IExercise?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public IPagedResult<IExercise> GetPagedAsync(IPagedRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var query = dbContext.Exercises
            .AsNoTracking()
            .AsQueryable();

        var isSortDescending = request.SortDirection == SortDirection.Descending;

        // Sort the results based on the sort column
        query = request.SortColumn?.ToLower() switch
        {
            "name" => isSortDescending
                ? query.OrderByDescending(b => b.Name)
                : query.OrderBy(b => b.Name),
            _ => isSortDescending
                ? query.OrderByDescending(b => b.Id)
            : query.OrderBy(b => b.Id)
        };

        var projectQuery = query.ProjectTo<IExercise>(mapper.ConfigurationProvider);

        return new PagedResult<IExercise>(projectQuery);
    }

    public async Task<IExercise> CreateAsync(IExerciseCreate exercise)
    {
        ArgumentNullException.ThrowIfNull(exercise);

        var dbEntity = new DbExercise
        {
            Name = exercise.Name,
            Description = exercise.Description,
            MuscleGroupId = exercise.MuscleGroupId,
            ExerciseLogType = exercise.ExerciseLogType,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        try
        {
            dbContext.Exercises.Add(dbEntity);
            await dbContext.SaveChangesAsync();

            logger.LogInformation($"Created Exercise with id {dbEntity.Id}");
            return dbEntity.MapTo<IExercise>(mapper);
        }
        catch (Exception ex)
        {
            throw new GymLoggerException($"Failed to create exercise {dbEntity.Name}", ex);
        }
    }

    public async Task UpdateAsync(IExerciseUpdate exercise)
    {
        ArgumentNullException.ThrowIfNull(exercise);

        var dbEntity = await dbContext.Exercises.FindAsync(exercise.Id);

        if (dbEntity == null)
        {
            throw new GymLoggerEntityNotFoundException($"No Exercise was found for id {exercise.Id}");
        }

        try
        {
            dbEntity.Name = exercise.Name;
            dbEntity.Description = exercise.Description;
            dbEntity.MuscleGroupId = exercise.MuscleGroupId;
            dbEntity.ExerciseLogType = exercise.ExerciseLogType;
            dbEntity.UpdatedAt = DateTime.Now;

            await dbContext.SaveChangesAsync();

            logger.LogInformation($"Updated Exercise with id {exercise.Id}");
        }
        catch (Exception ex)
        {
            throw new GymLoggerException($"Failed to update exercise {dbEntity.Name}", ex);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var dbEntity = await dbContext.Exercises.FindAsync(id);

        if (dbEntity == null)
        {
            throw new GymLoggerEntityNotFoundException($"No Exercise was found for id {id}");
        }

        try
        {
            dbContext.Exercises.Remove(dbEntity);
            await dbContext.SaveChangesAsync();

            logger.LogInformation($"Deleted Exercise with id {id}");
        }
        catch (Exception ex)
        {
            throw new GymLoggerException($"Failed to delete Exercise {dbEntity.Name}", ex);
        }
    }
}
