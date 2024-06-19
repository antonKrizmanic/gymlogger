using GymLogger.Shared.Models.Paging;

namespace GymLogger.Api.Client.HttpServices;

public interface IPagedHttpService<TDto, TRequest>
    where TDto : class
    where TRequest : PagedRequestDto
{
    Task<PagedResponseDto<TDto>> GetPagedListAsync(TRequest request);
}
