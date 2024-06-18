using GymLogger.Application.User.Interfaces;
using GymLogger.Core.User.Interfaces;
using GymLogger.Infrastructure.Database.CodeExtensions;
using GymLogger.Infrastructure.Database.Models.Exercise;
using GymLogger.Infrastructure.Database.Models.Identity;
using GymLogger.Infrastructure.Database.Models.MuscleGroups;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymLogger.Infrastructure.Database;
public class GymLoggerDbContext :
    IdentityDbContext<DbApplicationUser>
{

    private readonly string? CurrentUserId;

    public GymLoggerDbContext(DbContextOptions<GymLoggerDbContext> options, ICurrentUserProvider currentUserProvider) : base(options)
    {
        if(currentUserProvider is not null)
        {
            CurrentUserId = currentUserProvider.GetCurrentUserId();
        }
    }

    public virtual DbSet<DbMuscleGroup> MuscleGroups { get; set; }
    public virtual DbSet<DbExercise> Exercises { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.SetQueryFilterOnAllEntities<IBelongsToUser>(c => this.CurrentUserId != null && this.CurrentUserId == c.BelongsToUserId);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(
        ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 6);
    }
}
