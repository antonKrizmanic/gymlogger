using GymLogger.Application.User.Interfaces;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymLogger.Api.Endpoints.Common;

public static class ExerciseApiEndpoints
{
    public static WebApplication MapExerciseApiEndpoints(this WebApplication app, string apiUrl, string tag)
    {
        var group = app.MapGroup(apiUrl);

        group.MapGet("/", async ([AsParameters] ExercisePagedRequestDto pagedRequestDto, IExerciseApiService apiService, IHttpContextAccessor contextAccessor, ICurrentUserProvider currentUserProvider) =>
        {
            return await apiService.GetPagedListAsync(pagedRequestDto);
        })
            .Produces<PagedResponseDto<ExerciseDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id}", async ([FromRoute] Guid id, IExerciseApiService apiService) =>
        {
            return await apiService.GetAsync(id);
        })
            .Produces<ExerciseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", async ([FromBody] ExerciseCreateDto exerciseCreateDto, IExerciseApiService apiService) =>
        {
            return await apiService.CreateAsync(exerciseCreateDto);
        })
            .Produces<ExerciseDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("/{id:guid}", async ([FromRoute] Guid id, [FromBody] ExerciseUpdateDto exerciseUpdateDto, IExerciseApiService apiService) =>
        {
            if (id != exerciseUpdateDto.Id)
                return Results.BadRequest("Id mismatch");

            await apiService.UpdateAsync(exerciseUpdateDto);
            return Results.NoContent();
        })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("/{id:guid}", async ([FromRoute] Guid id, IExerciseApiService apiService) =>
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
