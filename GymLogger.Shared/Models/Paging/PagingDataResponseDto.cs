using GymLogger.Common.Enums;

namespace GymLogger.Shared.Models.Paging;
public record PagingDataResponseDto
{
    public int TotalItems { get; set; }
    public int Page { get; set; } = 0;
    public int PageSize { get; set; } = 12;
    public string? Search { get; set; }
    public string SortColumn { get; set; } = "Name";
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
}
