namespace GymLogger.Shared.Models.Paging;
public class PagedResponseDto<T>
{
    public PagingDataResponseDto? PagingData { get; set; }

    public ICollection<T>? Items { get; set; }
}
