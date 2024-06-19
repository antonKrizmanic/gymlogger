using GymLogger.Api.Client.Components;
using GymLogger.Api.Client.HttpServices.Exercise;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.Paging;
using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Exercises;

public partial class Index : BaseComponent
{
    [Inject] private IExerciseHttpService ExerciseHttpService { get; set; }

    private PagingDataResponseDto pagingDataResponseDto = new();
    private ICollection<ExerciseDto> items = new List<ExerciseDto>();
    private string? _searchValue;
    private PagedRequestDto _pagedRequestDto = new() { Page = 0, PageSize = 10, SortColumn = "Name", SortDirection = Common.Enums.SortDirection.Ascending };

    protected override async Task OnInitializedAsync()
    {
        var results = await ExerciseHttpService.GetPagedListAsync(_pagedRequestDto);
        this.pagingDataResponseDto = results.PagingData;
        this.items = results.Items;
    }

    private async Task OnSearchAsync()
    {
        if (!string.IsNullOrWhiteSpace(_searchValue))
        {
            // You can also call an API here if the list is not local.
            this._pagedRequestDto.Search = _searchValue;
            this._pagedRequestDto.Page = 0;
            var results = await ExerciseHttpService.GetPagedListAsync(this._pagedRequestDto);

            this.pagingDataResponseDto = results.PagingData;
            this.items = results.Items;
        }
        else
        {
            this._pagedRequestDto.Search = null;
            this._pagedRequestDto.Page = 0;
            var results = await ExerciseHttpService.GetPagedListAsync(_pagedRequestDto);
            this.pagingDataResponseDto = results.PagingData;
            this.items = results.Items;
        }
    }
}
