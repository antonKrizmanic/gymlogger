using GymLogger.Shared.Constants;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Services;
using System.Net.Http.Json;

namespace GymLogger.Api.Client.HttpServices.Exercise;

public class ExerciseHttpService(IHttpClientFactory httpClientFactory) : BaseHttpService(httpClientFactory, ApiRoutes.Exercise), IExerciseApiService
{
    public async Task<PagedResponseDto<ExerciseDto>> GetPagedListAsync(ExercisePagedRequestDto request)
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

    public async Task<ExerciseDto> CreateAsync(ExerciseCreateDto dto)
    {
        var response = await base.HttpClient.PostAsJsonAsync(base.ApiRoute, dto, Context.ExerciseCreateDto);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to create exercise. Status code: {response.StatusCode}");

        return await response.Content.ReadFromJsonAsync<ExerciseDto>();
    }

    public async Task UpdateAsync(ExerciseUpdateDto dto) =>
        await base.HttpClient.PutAsJsonAsync($"{base.ApiRoute}/{dto.Id}", dto, Context.ExerciseUpdateDto);

    public async Task DeleteAsync(Guid id) =>
        await base.HttpClient.DeleteAsync($"{base.ApiRoute}/{id}");
}
