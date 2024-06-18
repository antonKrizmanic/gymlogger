using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymLogger.Common.Enums;
using GymLogger.Core.Exercise;
using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Core.Paging.Interfaces;
using GymLogger.Infrastructure.Database.Models.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Infrastructure.Database.Models.Exercise;
internal class ExerciseRepository(GymLoggerDbContext dbContext, IMapper mapper) : IExerciseRepository
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
}
