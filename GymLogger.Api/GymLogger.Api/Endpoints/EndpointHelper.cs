namespace GymLogger.Api.Endpoints;

public static class EndpointHelper
{
    public static async Task<IResult> CheckResultForNull<T>(Task<T> func) =>
            await func
                is { } model
        ? Results.Ok(model)
        : Results.NotFound();

    public static RouteGroupBuilder AddAuthOpenApiAndTag(this RouteGroupBuilder group, string tag) =>
        group.RequireAuthorization()
            .WithOpenApi()
            .WithTags(tag);
}
