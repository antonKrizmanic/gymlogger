﻿using GymLogger.Application.Exercise;
using GymLogger.Application.Management;
using GymLogger.Application.MuscleGroups;
using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.Management.Interfaces;
using GymLogger.Core.MuscleGroups.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GymLogger.Application;
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IMuscleGroupService, MuscleGroupService>()
            .AddTransient<IManagementService, ManagementService>()
            .AddTransient<IExerciseService, ExerciseService>();

        return services;
    }
}
