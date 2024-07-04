using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GymLogger.Infrastructure.Database
{
    public class GymLoggerDbContextMigrationFactory : IDesignTimeDbContextFactory<GymLoggerDbContext>
    {
        public GymLoggerDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GymLoggerDbContext>();

            var connectionString = "Server=localhost;Database=GymLoggerDb;Port=5432;User Id=postgres;Password=postgres;";

            builder.UseNpgsql(connectionString);

            return new GymLoggerDbContext(builder.Options, null, null);
        }
    }
}
