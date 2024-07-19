using GymLogger.Shared.Models.ExerciseWorkout;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymLogger.Api.Endpoints.Common;

public static class ExerciseWorkoutApiEndpoints
{

    public static WebApplication MapExerciseWorkoutApiEndpoints(this WebApplication app, string apiUrl, string tag)
    {
        var group = app.MapGroup(apiUrl);

        group.MapGet("/GetLatest/{id}/{workoutId}", async ([FromRoute] Guid id, [FromRoute] Guid workoutId, IExerciseWorkoutApiService apiService) =>
        {
            return await apiService.GetLatestForCurrentUser(id, workoutId);
        })
            .Produces<PagedResponseDto<ExerciseWorkoutDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);


        group
            .RequireAuthorization()
            .WithOpenApi()
            .WithTags(tag);

        return app;
    }

}
