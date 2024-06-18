using GymLogger.Core.Management.Interfaces;
using Serilog;

namespace GymLogger.Api.Services.Management;

public class ManagementApiService(IManagementService managementService, Serilog.ILogger logger) : IManagementApiService
{
    public async Task<bool> AssertMigrationsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await managementService.AssertMigrationsAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            logger.Warning(ex, "Failed to assert migrations");

            return false;
        }
    }

    public async Task<bool> ExecuteMigrationsAsync(string? targetMigration = null, CancellationToken cancellationToken = default)
    {
        try
        {
            await managementService.ExecuteMigrationsAsync(targetMigration, cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            logger.Warning(ex, "Failed to execute migrations");

            return false;
        }
    }

    public async Task<bool> ExecuteSeedAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await managementService.SeedDatabaseAsync();

            return true;
        }
        catch (Exception ex)
        {
            logger.Warning(ex, "Failed to seed database");
            return false;
        }
    }
}
