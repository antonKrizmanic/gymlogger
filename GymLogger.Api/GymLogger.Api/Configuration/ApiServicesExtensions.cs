using GymLogger.Api.Services.Exercise;
using GymLogger.Api.Services.ExerciseWorkout;
using GymLogger.Api.Services.Management;
using GymLogger.Api.Services.MuscleGroups;
using GymLogger.Api.Services.Workout;
using GymLogger.Shared.Services;

namespace GymLogger.Api.Configuration;

public static class ApiServicesExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddTransient<IManagementApiService, ManagementApiService>()
            .AddTransient<IMuscleGroupApiService, MuscleGroupApiService>()
            .AddTransient<IExerciseApiService, ExerciseApiService>()
            .AddTransient<IWorkoutApiService, WorkoutApiService>()
            .AddTransient<IExerciseWorkoutApiService, ExerciseWorkoutApiService>();

        return services;
    }
}
