using GymLogger.Application.User.Interfaces;
using GymLogger.Core.User.Interfaces;
using GymLogger.Infrastructure.Database.CodeExtensions;
using GymLogger.Infrastructure.Database.Models.Exercise;
using GymLogger.Infrastructure.Database.Models.ExerciseSet;
using GymLogger.Infrastructure.Database.Models.ExerciseWorkout;
using GymLogger.Infrastructure.Database.Models.Identity;
using GymLogger.Infrastructure.Database.Models.MuscleGroups;
using GymLogger.Infrastructure.Database.Models.Workout;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace GymLogger.Infrastructure.Database;
public class GymLoggerDbContext :
    IdentityDbContext<DbApplicationUser>
{
    private readonly ICurrentUserProvider currentUserProvider;

    public GymLoggerDbContext(DbContextOptions<GymLoggerDbContext> options, ICurrentUserProvider currentUserProvider, ILogger<GymLoggerDbContext> logger) : base(options)
    {
        this.currentUserProvider = currentUserProvider;
    }

    public virtual DbSet<DbMuscleGroup> MuscleGroups { get; set; }
    public virtual DbSet<DbExercise> Exercises { get; set; }
    public virtual DbSet<DbWorkout> Workouts { get; set; }
    public virtual DbSet<DbExerciseWorkout> ExerciseWorkouts { get; set; }
    public virtual DbSet<DbExerciseSet> ExerciseSets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.SetQueryFilterOnAllEntities<IBelongsToUser>(c => (this.currentUserProvider.GetCurrentUserId() != null && this.currentUserProvider.GetCurrentUserId() == c.BelongsToUserId) || c.BelongsToUserId == null);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(
        ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 6);
    }
}
