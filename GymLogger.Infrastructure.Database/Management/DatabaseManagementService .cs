using GymLogger.Core.Management.Interfaces;
using GymLogger.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GymLogger.Infrastructure.Database.Management;

public class DatabaseManagementService(GymLoggerDbContext context, IDatabaseSeedService databaseSeedService, ILogger logger) : IDatabaseManagementService
{
    public async Task AssertMigrationsAsync(CancellationToken cancellationToken = default)
    {
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync(cancellationToken);
        var pendingMigrationsList = pendingMigrations.ToList();

        if (pendingMigrationsList.Any())
        {
            var pendingMigrationsListNames = string.Join(", ", pendingMigrationsList);

            throw new GymLoggerException(
                $"Following migrations are waiting to be applied: {pendingMigrationsListNames}");
        }
    }

    public async Task ExecuteMigrationsAsync(string? targetMigration = null, CancellationToken cancellationToken = default)
    {
        string? finalMigration = targetMigration;

        // Migrate to the latest migration if the explicit migration is empty
        if (string.IsNullOrWhiteSpace(finalMigration))
        {
            await context.Database.MigrateAsync(cancellationToken);
            finalMigration = "Latest";
        }
        else if (finalMigration == "InitialMigration")
        {
            await context.Database.EnsureDeletedAsync(cancellationToken);
            await MigrateToTargetMigrationAsync(finalMigration);
            finalMigration = "InitialMigration";
        }
        else
        {
            await MigrateToTargetMigrationAsync(finalMigration);
        }

        await databaseSeedService.SeedDatabaseAsync();

        logger.LogInformation($"Database successfully migrated to {finalMigration}");
    }

    private async Task MigrateToTargetMigrationAsync(string targetMigration)
    {
        var isValidMigration = context.Database
            .GetMigrations()
            .Any(x => x[x.IndexOf('_')..].Contains(targetMigration));

        // Check if migration valid
        if (!isValidMigration)
        {
            throw new InvalidOperationException($"{targetMigration} is not a valid migration name!");
        }

        var migrator = context.GetInfrastructure().GetService<IMigrator>();

        // Check if resolved
        if (migrator == null)
        {
            throw new GymLoggerException($"Migrator service could not be resolved");
        }

        await migrator.MigrateAsync(targetMigration);
    }
}