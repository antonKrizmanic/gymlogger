using GymLogger.Common.Enums;

namespace GymLogger.Shared.Models.Paging;
public class PagedRequestDto
{
    public int Page { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public string SortColumn { get; set; } = "Id";
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
}
