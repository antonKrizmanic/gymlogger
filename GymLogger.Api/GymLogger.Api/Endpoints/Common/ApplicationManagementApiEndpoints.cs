using GymLogger.Api.Services.Management;
using Microsoft.AspNetCore.Mvc;

namespace GymLogger.Api.Endpoints.Common;

public static class ApplicationManagementApiEndpoints
{
    public static WebApplication MapApplicationManagerApiEndpoints(this WebApplication app, string apiUrl, string tag)
    {
        var group = app.MapGroup(apiUrl);

        //group.MapGet("/assert-migrations", async (IManagementApiService apiService) =>
        //{
        //    return await apiService.AssertMigrationsAsync();
        //});

        //group.MapGet("/execute-migrations", async ([FromQuery]string? targetMigrations, IManagementApiService apiService) =>
        //{
        //    return await apiService.ExecuteMigrationsAsync(targetMigrations);
        //});

        group.MapGet("/execute-seed", async (IManagementApiService apiService) =>
        {
            return await apiService.ExecuteSeedAsync();
        });

        group
            .WithOpenApi()
            .WithTags(tag);

        return app;
    }
}
