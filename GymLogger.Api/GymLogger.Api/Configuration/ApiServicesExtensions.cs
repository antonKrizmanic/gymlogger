using GymLogger.Api.Services.Exercise;
using GymLogger.Api.Services.Management;
using GymLogger.Api.Services.MuscleGroups;
using GymLogger.Shared.Services;

namespace GymLogger.Api.Configuration;

public static class ApiServicesExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddTransient<IManagementApiService, ManagementApiService>()
            .AddTransient<IMuscleGroupApiService, MuscleGroupApiService>()
            .AddTransient<IExerciseApiService, ExerciseApiService>();

        return services;
    }
}
