using GymLogger.Shared.Constants;
using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Services;
using System.Net.Http.Json;

namespace GymLogger.Api.Client.HttpServices.MuscleGroup;

public class MuscleGroupHttpService(IHttpClientFactory httpClientFactory) : BaseHttpService(httpClientFactory, ApiRoutes.MuscleGroup), IMuscleGroupApiService
{
    public async Task<IEnumerable<MuscleGroupDto>> GetAllAsync()
    {
        var request = new PagedRequestDto() { PageSize = int.MaxValue };
        var url = GetQueryParameterDictionary($"{this.ApiRoute}", request);
        var result = await base.HttpClient.GetFromJsonAsync(url, Context.PagedResponseDtoMuscleGroupDto) ??
            throw new Exception("Paged response dto is null");
        return result.Items;
    }

    public Task<MuscleGroupDto> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResponseDto<MuscleGroupDto>> GetPagedAsync(PagedRequestDto pagedRequestDto)
    {
        throw new NotImplementedException();
    }
}
