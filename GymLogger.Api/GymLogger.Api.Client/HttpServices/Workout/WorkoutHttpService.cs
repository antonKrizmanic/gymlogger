using GymLogger.Shared.Constants;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Models.Workout;
using GymLogger.Shared.Services;
using System.Net.Http.Json;

namespace GymLogger.Api.Client.HttpServices.Workout;

public class WorkoutHttpService(IHttpClientFactory httpClientFactory) : BaseHttpService(httpClientFactory, ApiRoutes.Workout), IWorkoutApiService
{
    public async Task<PagedResponseDto<WorkoutDto>> GetPagedListAsync(WorkoutPagedRequestDto request)
    {
        var url = GetQueryParameterDictionary($"{this.ApiRoute}", request);
        return await base.HttpClient.GetFromJsonAsync(url, Context.PagedResponseDtoWorkoutDto) ??
            throw new Exception("Paged response dto is null");
    }

    public async Task<WorkoutDto> GetAsync(Guid id) =>
        await base.HttpClient.GetFromJsonAsync($"{base.ApiRoute}/{id}", Context.WorkoutDto) ??
        throw new Exception("Workout Dto is null");

    public async Task<WorkoutDto> CreateAsync(WorkoutCreateDto dto)
    {
        var response = await base.HttpClient.PostAsJsonAsync(base.ApiRoute, dto, Context.WorkoutCreateDto);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to create workout. Status code: {response.StatusCode}");

        return await response.Content.ReadFromJsonAsync<WorkoutDto>();
    }

    public async Task UpdateAsync(WorkoutUpdateDto dto)
    {
        var response = await base.HttpClient.PutAsJsonAsync($"{base.ApiRoute}/{dto.Id}", dto, Context.WorkoutUpdateDto);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to update workout. Status code: {response.StatusCode}");
    }

    public async Task DeleteAsync(Guid id)
    {
        var response = await base.HttpClient.DeleteAsync($"{base.ApiRoute}/{id}");

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to delete workout. Status code: {response.StatusCode}");
    }
}
