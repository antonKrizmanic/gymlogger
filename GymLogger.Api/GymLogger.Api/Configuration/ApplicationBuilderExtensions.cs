using GymLogger.Api.Endpoints.Common;

namespace GymLogger.Api.Configuration;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseMinimalApi(this WebApplication app)
    {
        app.UseApplicationManagerApi();

        return app;
    }
}
