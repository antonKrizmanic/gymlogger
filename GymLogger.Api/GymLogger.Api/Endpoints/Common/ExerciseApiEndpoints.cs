using GymLogger.Api.Services.Exercise;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.Paging;
using Microsoft.AspNetCore.Mvc;

namespace GymLogger.Api.Endpoints.Common;

public static class ExerciseApiEndpoints
{
    public static WebApplication MapExerciseApiEndpoints(this WebApplication app, string apiUrl, string tag)
    {
        var group = app.MapGroup(apiUrl);

        group.MapGet("/", async ([AsParameters] PagedRequestDto pagedRequestDto, IExerciseApiService apiService) =>
        {
            return await apiService.GetPagedAsync(pagedRequestDto);
        })
            .Produces<PagedResponseDto<ExerciseDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id}", async ([FromRoute] Guid id, IExerciseApiService apiService) =>
        {
            return await apiService.GetById(id);
        })
            .Produces<ExerciseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group
            .WithOpenApi()
            .WithTags(tag);

        return app;
    }
}
