using GymLogger.Shared.Constants;
using GymLogger.Shared.Models.ExerciseWorkout;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Services;
using System.Net.Http.Json;

namespace GymLogger.Api.Client.HttpServices.ExerciseWorkout;

internal class ExerciseWorkoutHttpService(IHttpClientFactory httpClientFactory) : BaseHttpService(httpClientFactory, ApiRoutes.ExerciseWorkout), IExerciseWorkoutApiService
{
    public async Task<ExerciseWorkoutDto?> GetLatestForCurrentUserAsync(Guid exerciseId, Guid? workoutId = null) =>
        await base.HttpClient.GetFromJsonAsync($"{base.ApiRoute}/GetLatest/{exerciseId}/{workoutId}", Context.ExerciseWorkoutDto);

    public async Task<PagedResponseDto<ExerciseWorkoutDetailDto>> GetPagedListAsync(ExerciseWorkoutPagedRequestDto request)
    {
        var url = GetQueryParameterDictionary($"{this.ApiRoute}", request);
        return await base.HttpClient.GetFromJsonAsync(url, Context.PagedResponseDtoExerciseWorkoutDetailDto) ??
            throw new Exception("Paged response dto is null");
    }
}
