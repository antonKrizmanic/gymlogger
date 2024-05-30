namespace GymLogger.Api.Endpoints;

public static class EndpointHelper
{
    public static async Task<IResult> CheckResultForNull<T>(Task<T> func) =>
            await func
                is { } model
        ? Results.Ok(model)
        : Results.NotFound();

    // TODO: Uncomment when Internet is on
    public static RouteGroupBuilder AddAuthOpenApiAndTag(this RouteGroupBuilder builder, string tag) =>
        builder.RequireAuthorization();
        //.WithOpenApi()
        //.WithTags(tag);
}
