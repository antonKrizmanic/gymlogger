using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymLogger.Api.Endpoints.Common;

public static class MuscleGroupApiEndpoints
{
    public static WebApplication MapMuscleGroupApiEndpoints(this WebApplication app, string apiUrl, string tag)
    {
        var group = app.MapGroup(apiUrl);

        group.MapGet("/", async ([AsParameters] PagedRequestDto pagedRequestDto, IMuscleGroupApiService apiService) =>
        {
            return await apiService.GetPagedAsync(pagedRequestDto);
        })
            .Produces<PagedResponseDto<MuscleGroupDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id}", async ([FromRoute] Guid id, IMuscleGroupApiService apiService) =>
        { 
            return await apiService.GetById(id);
        })
            .Produces<MuscleGroupDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group
            .WithOpenApi()
            .WithTags(tag);

        return app;
    }
}
