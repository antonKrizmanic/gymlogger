namespace GymLogger.Core.Management.Interfaces;
public interface IDatabaseManagementService
{
    Task AssertMigrationsAsync(CancellationToken cancellationToken = default);
    Task ExecuteMigrationsAsync(string? targetMigration = null, CancellationToken cancellationToken = default);
}
