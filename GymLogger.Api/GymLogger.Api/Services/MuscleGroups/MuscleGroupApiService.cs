using AutoMapper;
using GymLogger.Common.Enums;
using GymLogger.Core.CodeExtensions;
using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Core.Paging;
using GymLogger.Core.Paging.Interfaces;
using GymLogger.Exceptions;
using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Services;

namespace GymLogger.Api.Services.MuscleGroups;

internal class MuscleGroupApiService(IMuscleGroupService muscleGroupService, IMapper mapper) : IMuscleGroupApiService
{
    public async Task<IEnumerable<MuscleGroupDto>> GetAllAsync()
    {
        var pagedRequest = new PagedRequest
        {
            Page = 0,
            PageSize = int.MaxValue,
            SortColumn = "Name",
            SortDirection = SortDirection.Ascending
        };
        var pagedMuscleGroups = muscleGroupService.GetPagedAsync(pagedRequest);
        var pagedItems = await pagedMuscleGroups.GetPageAsync(pagedRequest.Page, pagedRequest.PageSize);

        return pagedItems.MapTo<ICollection<MuscleGroupDto>>(mapper);
    }

    public async Task<MuscleGroupDto> GetById(Guid id)
    {
        var entity = await muscleGroupService.GetByIdAsync(id);

        if (entity == null)
        {
            throw new GymLoggerEntityNotFoundException($"No muscle group was found for id {id}");
        }

        return entity.MapTo<MuscleGroupDto>(mapper);
    }

    public async Task<PagedResponseDto<MuscleGroupDto>> GetPagedAsync(PagedRequestDto pagedRequestDto)
    {
        var pagedRequest = pagedRequestDto.MapTo<IPagedRequest>(mapper);
        var pagedMuscleGroups = muscleGroupService.GetPagedAsync(pagedRequest);

        var totalItems = await pagedMuscleGroups.TotalCountAsync();
        var pagedItems = await pagedMuscleGroups.GetPageAsync(pagedRequestDto.Page, pagedRequestDto.PageSize);

        return new PagedResponseDto<MuscleGroupDto>
        {
            PagingData = new PagingDataResponseDto
            {
                Page = pagedRequestDto.Page,
                PageSize = pagedRequestDto.PageSize,
                SortColumn = pagedRequestDto.SortColumn,
                SortDirection = pagedRequestDto.SortDirection,
                TotalItems = totalItems
            },
            Items = pagedItems.MapTo<ICollection<MuscleGroupDto>>(mapper),
        };
    }
}
