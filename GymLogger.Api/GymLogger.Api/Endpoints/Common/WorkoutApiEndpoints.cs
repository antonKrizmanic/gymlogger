using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Models.Workout;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymLogger.Api.Endpoints.Common;

public static class WorkoutApiEndpoints
{
    public static WebApplication MapWorkoutApiEndpoints(this WebApplication app, string apiUrl, string tag)
    {
        var group = app.MapGroup(apiUrl);

        group.MapGet("/", async ([AsParameters] WorkoutPagedRequestDto pagedRequestDto, IWorkoutApiService apiService) =>
        {
            return await apiService.GetPagedListAsync(pagedRequestDto);
        })
            .Produces<PagedResponseDto<WorkoutDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id}", async ([FromRoute] Guid id, IWorkoutApiService apiService) =>
        {
            return await apiService.GetAsync(id);
        })
            .Produces<WorkoutDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", async ([FromBody] WorkoutCreateDto workoutCreateDto, IWorkoutApiService apiService) =>
        {
            return await apiService.CreateAsync(workoutCreateDto);
        })
            .Produces<WorkoutDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("/{id:guid}", async ([FromRoute] Guid id, [FromBody] WorkoutUpdateDto exerciseUpdateDto, IWorkoutApiService apiService) =>
        {
            if (id != exerciseUpdateDto.Id)
                return Results.BadRequest("Id mismatch");

            await apiService.UpdateAsync(exerciseUpdateDto);
            return Results.NoContent();
        })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("/{id:guid}", async ([FromRoute] Guid id, IWorkoutApiService apiService) =>
        {
            await apiService.DeleteAsync(id);

            return Results.NoContent();
        })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group
            .RequireAuthorization()
            .WithOpenApi()
            .WithTags(tag);

        return app;
    }
}
