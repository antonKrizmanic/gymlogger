using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymLogger.Common.Enums;
using GymLogger.Core.CodeExtensions;
using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Core.Paging.Interfaces;
using GymLogger.Infrastructure.Database.Models.Paging;
using Microsoft.EntityFrameworkCore;

namespace GymLogger.Infrastructure.Database.Models.MuscleGroups;
internal class MuscleGroupsRepository(GymLoggerDbContext dbContext, IMapper mapper) : IMuscleGroupsRepository
{
    public IPagedResult<IMuscleGroup> GetPagedAsync(IPagedRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var query = dbContext.MuscleGroups
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

        var projectQuery = query.ProjectTo<IMuscleGroup>(mapper.ConfigurationProvider);

        return new PagedResult<IMuscleGroup>(projectQuery);
    }

    public async Task<IMuscleGroup?> GetByIdAsync(Guid id)
    {
        var dbMuscleGroup = await dbContext.MuscleGroups.FirstOrDefaultAsync(x => x.Id == id);

        return dbMuscleGroup?.MapTo<IMuscleGroup>(mapper);
    }
}
