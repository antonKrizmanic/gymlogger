using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Models.Paging;

namespace GymLogger.Api.Services.MuscleGroups;

public interface IMuscleGroupsApiService
{
    Task<PagedResponseDto<MuscleGroupDto>> GetPagedAsync(PagedRequestDto pagedRequestDto);

    Task<MuscleGroupDto> GetById(Guid id);
}
