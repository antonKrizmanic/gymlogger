using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Models.Paging;

namespace GymLogger.Shared.Services;

public interface IMuscleGroupApiService
{
    Task<IEnumerable<MuscleGroupDto>> GetAllAsync();
    Task<PagedResponseDto<MuscleGroupDto>> GetPagedAsync(PagedRequestDto pagedRequestDto);

    Task<MuscleGroupDto> GetById(Guid id);
}
