using GymLogger.Api.Services.Management;

namespace GymLogger.Api.Configuration;

public static class ApiServicesExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddTransient<IManagementApiService, ManagementApiService>();

        return services;
    }
}
