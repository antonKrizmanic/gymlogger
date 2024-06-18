using GymLogger.Common.Enums;
using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Core.Paging;
internal class PagedRequest : IPagedRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Search { get; set; }
    public string? SortColumn { get; set; }
    public SortDirection SortDirection { get; set; }
}
