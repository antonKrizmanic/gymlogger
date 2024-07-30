using GymLogger.Shared.Models.Dashboard;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Services;

namespace GymLogger.Api.Endpoints.Common;

public static class DashboardApiEndpoints
{
    public static WebApplication MapDashboardApiEndpoints(this WebApplication app, string apiUrl, string tag)
    {
        var group = app.MapGroup(apiUrl);

        group.MapGet("/", async (IDashboardApiService apiService) =>
        {
            return await apiService.GetAsync();
        })
            .Produces<PagedResponseDto<DashboardDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group
            .RequireAuthorization()
            .WithOpenApi()
            .WithTags(tag);

        return app;
    }
}
