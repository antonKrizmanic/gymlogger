﻿using GymLogger.Api.Services.Exercise;
using GymLogger.Api.Services.Management;
using GymLogger.Api.Services.MuscleGroups;

namespace GymLogger.Api.Configuration;

public static class ApiServicesExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddTransient<IManagementApiService, ManagementApiService>()
            .AddTransient<IMuscleGroupsApiService, MuscleGroupsApiService>()
            .AddTransient<IExerciseApiService, ExerciseApiService>();

        return services;
    }
}
