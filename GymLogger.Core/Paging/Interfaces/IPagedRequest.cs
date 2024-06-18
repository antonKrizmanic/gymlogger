using GymLogger.Common.Enums;

namespace GymLogger.Core.Paging.Interfaces;
public interface IPagedRequest
{
    int Page { get; set; }
    int PageSize { get; set; }
    string? Search { get; set; }
    string? SortColumn { get; set; }
    SortDirection SortDirection { get; set; }
}
