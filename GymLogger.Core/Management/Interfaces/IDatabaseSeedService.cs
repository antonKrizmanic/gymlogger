using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Core.Management.Interfaces;
public interface IDatabaseSeedService
{
    Task SeedDatabaseAsync();
}
