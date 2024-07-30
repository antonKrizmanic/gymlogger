using GymLogger.Core.Dashboards.Interfaces;
using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.ExerciseWorkout.Interfaces;
using GymLogger.Core.Management.Interfaces;
using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Core.Workout.Interfaces;
using GymLogger.Infrastructure.Database.Dashboards;
using GymLogger.Infrastructure.Database.Management;
using GymLogger.Infrastructure.Database.Models.Exercise;
using GymLogger.Infrastructure.Database.Models.ExerciseWorkout;
using GymLogger.Infrastructure.Database.Models.Identity;
using GymLogger.Infrastructure.Database.Models.MuscleGroups;
using GymLogger.Infrastructure.Database.Models.Workout;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GymLogger.Infrastructure.Database;
public static class InfrastructureDatabaseServicesExtensions
{
    public static IServiceCollection RegisterInfrastructureDbServices(this IServiceCollection services)
    {
        services.AddTransient<IDatabaseSeedService, DatabaseSeedService>()
            .AddTransient<IDatabaseManagementService, DatabaseManagementService>();
        return services;
    }

    public static IServiceCollection RegisterInfrastructureDbRepositories(this IServiceCollection services)
    {
        services
            .AddTransient<IMuscleGroupsRepository, MuscleGroupsRepository>()
            .AddTransient<IExerciseRepository, ExerciseRepository>()
            .AddTransient<IWorkoutRepository, WorkoutRepository>()
            .AddTransient<IExerciseWorkoutRepository, ExerciseWorkoutRepository>()
            .AddTransient<IDashboardRepository, DashboardRepository>();
        return services;
    }

    public static IServiceCollection AddInfrastructureDatabase(this IServiceCollection services,
        IConfiguration configuration, bool isProduction, bool isTestEnv = false)
    {
        var connectionStringKey = isProduction ? "ProductionConnection" : "DefaultConnection";
        var connectionString = configuration.GetConnectionString(connectionStringKey);
        services.AddDbContext<GymLoggerDbContext>(
            options => options
                .UseNpgsql(connectionString)
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .EnableSensitiveDataLogging(),
            isTestEnv ? ServiceLifetime.Transient : ServiceLifetime.Scoped)
            ;
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddIdentityCore<DbApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<GymLoggerDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        return services;
    }
}
