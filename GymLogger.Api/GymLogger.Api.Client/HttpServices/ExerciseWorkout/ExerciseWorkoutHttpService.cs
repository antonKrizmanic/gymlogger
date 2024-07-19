using GymLogger.Shared.Constants;
using GymLogger.Shared.Models.ExerciseWorkout;
using GymLogger.Shared.Services;
using System.Net.Http.Json;

namespace GymLogger.Api.Client.HttpServices.ExerciseWorkout;

internal class ExerciseWorkoutHttpService(IHttpClientFactory httpClientFactory) : BaseHttpService(httpClientFactory, ApiRoutes.ExerciseWorkout), IExerciseWorkoutApiService
{
    public async Task<ExerciseWorkoutDto?> GetLatestForCurrentUser(Guid exerciseId, Guid? workoutId = null) =>
        await base.HttpClient.GetFromJsonAsync($"{base.ApiRoute}/GetLatest/{exerciseId}/{workoutId}", Context.ExerciseWorkoutDto);
}
