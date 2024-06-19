using GymLogger.Shared.Constants;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.Paging;
using System.Net.Http.Json;

namespace GymLogger.Api.Client.HttpServices.Exercise;

public class ExerciseHttpService(IHttpClientFactory httpClientFactory) : BaseHttpService(httpClientFactory, ApiRoutes.Exercise), IExerciseHttpService
{
    public async Task<PagedResponseDto<ExerciseDto>> GetPagedListAsync(PagedRequestDto request)
    {
        var url = GetQueryParameterDictionary($"{this.ApiRoute}", request);
        return await base.HttpClient.GetFromJsonAsync(url, Context.PagedResponseDtoExerciseDto) ??
            throw new Exception("Paged response dto is null");
    }

    public async Task<ExerciseDto> GetAsync(Guid id) =>
        await base.HttpClient.GetFromJsonAsync($"{base.ApiRoute}/{id}", Context.ExerciseDto) ??
        throw new Exception("Exercise Dto is null");

    public async Task<ExerciseUpdateDto> GetForEditAsync(Guid id) =>
        await base.HttpClient.GetFromJsonAsync($"{base.ApiRoute}/{id}", Context.ExerciseUpdateDto) ??
        throw new Exception("Exercise update dto is null");

    public async Task<HttpResponseMessage> CreateAsync(ExerciseCreateDto dto) =>
        await base.HttpClient.PostAsJsonAsync(base.ApiRoute, dto, Context.ExerciseCreateDto);

    public async Task<HttpResponseMessage> UpdateAsync(ExerciseUpdateDto dto) =>
        await base.HttpClient.PutAsJsonAsync($"{base.ApiRoute}/{dto.Id}", dto, Context.ExerciseUpdateDto);

    public async Task DeleteAsync(Guid id) =>
        await base.HttpClient.DeleteAsync($"{base.ApiRoute}/{id}");
}
