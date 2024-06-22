using GymLogger.Api.Client.HttpServices.Exercise;
using GymLogger.Api.Client.HttpServices.MuscleGroup;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace GymLogger.Api.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomHttpClientFactory(this IServiceCollection services, WebAssemblyHostBuilder builder)
    {
        services.AddHttpClient("BlazorServerHttpClient",
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
        return services;
    }

    public static IServiceCollection AddHttpServices(this IServiceCollection services)
    {
        services.AddScoped<IExerciseApiService, ExerciseHttpService>()
            .AddScoped<IMuscleGroupApiService, MuscleGroupHttpService>();
        return services;
    }
}
