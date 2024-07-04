namespace GymLogger.Core.Paging.Interfaces;
public interface IPagedResult<T>
{
    Task<IEnumerable<T>> GetPageAsync(int page, int pageSize);
    Task<int> TotalCountAsync();
}
