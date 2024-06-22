using GymLogger.Shared.Models.Paging;

namespace GymLogger.Shared.Services.Generics;

public interface IPagedHttpService<TDto, TRequest>
    where TDto : class
    where TRequest : PagedRequestDto
{
    Task<PagedResponseDto<TDto>> GetPagedListAsync(TRequest request);
}
