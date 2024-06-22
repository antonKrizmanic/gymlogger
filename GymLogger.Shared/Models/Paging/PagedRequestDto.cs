using GymLogger.Common.Enums;

namespace GymLogger.Shared.Models.Paging;
public class PagedRequestDto
{
    public int Page { get; set; } = 0;
    public int PageSize { get; set; } = 12;
    public string? Search { get; set; }
    public string SortColumn { get; set; } = "Name";
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
}
