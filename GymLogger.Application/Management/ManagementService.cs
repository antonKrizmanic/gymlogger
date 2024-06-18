using GymLogger.Core.Management.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Application.Management;
public class ManagementService(IDatabaseManagementService databaseManagementService, IDatabaseSeedService databaseSeedService) : IManagementService
{
    public Task AssertMigrationsAsync(CancellationToken cancellationToken = default) => 
        databaseManagementService.AssertMigrationsAsync(cancellationToken);

    public Task ExecuteMigrationsAsync(string? targetMigration = null, CancellationToken cancellationToken = default) => 
        databaseManagementService.ExecuteMigrationsAsync(targetMigration, cancellationToken);

    public Task SeedDatabaseAsync() => 
        databaseSeedService.SeedDatabaseAsync();
}
