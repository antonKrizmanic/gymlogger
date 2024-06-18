using GymLogger.Core.Management.Interfaces;

namespace GymLogger.Application.Management;
internal class ManagementService(IDatabaseManagementService databaseManagementService, IDatabaseSeedService databaseSeedService) : IManagementService
{
    public Task AssertMigrationsAsync(CancellationToken cancellationToken = default) => 
        databaseManagementService.AssertMigrationsAsync(cancellationToken);

    public Task ExecuteMigrationsAsync(string? targetMigration = null, CancellationToken cancellationToken = default) => 
        databaseManagementService.ExecuteMigrationsAsync(targetMigration, cancellationToken);

    public Task SeedDatabaseAsync() => 
        databaseSeedService.SeedDatabaseAsync();
}
