using AutoMapper;
using GymLogger.Core.CodeExtensions;
using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Core.Paging.Interfaces;
using GymLogger.Exceptions;
using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Models.Paging;

namespace GymLogger.Api.Services.MuscleGroups;

internal class MuscleGroupsApiService(IMuscleGroupService muscleGroupService, IMapper mapper) : IMuscleGroupsApiService
{
    public async Task<MuscleGroupDto> GetById(Guid id)
    {
        var book = await muscleGroupService.GetByIdAsync(id);

        if(book == null)
        {
            throw new GymLoggerEntityNotFoundException($"No muscle group was found for id {id}");
        }

        return book.MapTo<MuscleGroupDto>(mapper);
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
            Items = pagedItems.MapTo<IEnumerable<MuscleGroupDto>>(mapper),            
        };
    }
}
