using GymLogger.Application.Dashboard;
using GymLogger.Application.Exercise;
using GymLogger.Application.ExerciseWorkout;
using GymLogger.Application.Management;
using GymLogger.Application.MuscleGroups;
using GymLogger.Application.Workout;
using GymLogger.Core.Dashboards.Interfaces;
using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.ExerciseWorkout.Interfaces;
using GymLogger.Core.Management.Interfaces;
using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Core.Workout.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GymLogger.Application;
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddTransient<IDashboardService, DashboardService>()
            .AddTransient<IMuscleGroupService, MuscleGroupService>()
            .AddTransient<IManagementService, ManagementService>()
            .AddTransient<IExerciseService, ExerciseService>()
            .AddTransient<IWorkoutService, WorkoutService>()
            .AddTransient<IExerciseWorkoutService, ExerciseWorkoutService>();

        return services;
    }
}
