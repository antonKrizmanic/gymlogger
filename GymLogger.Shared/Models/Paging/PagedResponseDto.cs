namespace GymLogger.Shared.Models.Paging;
public class PagedResponseDto<T>
{
    public PagingDataResponseDto PagingData { get; set; }

    public IEnumerable<T> Items { get; set; }
}
