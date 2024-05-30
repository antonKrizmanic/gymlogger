namespace GymLogger.Api.Endpoints.Common;

public static class ApplicationManagementApiEndpoints
{
    public static WebApplication MapApplicationManagerApiEndpoints(this WebApplication app, string apiUrl, string tag)
    {
        var group = app.MapGroup(apiUrl);

        group.MapGet("/assert-migrations", async () =>
        {

        });

        group.MapGet("/execute-migrations", async () =>
        {

        });

        group.MapGet("/execute-seed", async () =>
        {

        });

        //group
        //    .WithOpenApi()
        //    .WithTags(tag);

        return app;
    }
}
