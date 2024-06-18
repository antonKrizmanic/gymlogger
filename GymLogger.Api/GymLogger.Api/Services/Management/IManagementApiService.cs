namespace GymLogger.Api.Services.Management;

public interface IManagementApiService
{
    Task<bool> AssertMigrationsAsync(CancellationToken cancellationToken = default);

    Task<bool> ExecuteMigrationsAsync(string? targetMigration = null, CancellationToken cancellationToken = default);

    Task<bool> ExecuteSeedAsync(CancellationToken cancellationToken = default);
}
