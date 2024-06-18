using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.Paging;

namespace GymLogger.Api.Services.Exercise;

public interface IExerciseApiService
{
    Task<PagedResponseDto<ExerciseDto>> GetPagedAsync(PagedRequestDto pagedRequestDto);

    Task<ExerciseDto> GetById(Guid id);

}
