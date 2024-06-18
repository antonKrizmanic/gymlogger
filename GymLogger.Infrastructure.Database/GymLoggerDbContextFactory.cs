using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GymLogger.Infrastructure.Database
{
    public class GymLoggerDbContextFactory : IDesignTimeDbContextFactory<GymLoggerDbContext>
    {
        public GymLoggerDbContext CreateDbContext(string[] args)
        {            
            var builder = new DbContextOptionsBuilder<GymLoggerDbContext>();

            var connectionString = "Server=localhost;Database=GymLoggerDb;Trusted_Connection=True;Encrypt=False;MultipleActiveResultSets=true";

            builder.UseSqlServer(connectionString);

            return new GymLoggerDbContext(builder.Options, null);
        }
    }
}
