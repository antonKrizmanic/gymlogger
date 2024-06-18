using GymLogger.Core.Management.Interfaces;
using GymLogger.Infrastructure.Database.Models.MuscleGroups;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Infrastructure.Database.Management;
internal class DatabaseSeedService(GymLoggerDbContext dbContext, ILogger logger) : IDatabaseSeedService
{
    public async Task SeedDatabaseAsync()
    {
        try
        {
            await this.SeedMuscleGroupsAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to seed database");
            throw;
        }
    }

    private async Task SeedMuscleGroupsAsync()
    {
        if (!dbContext.MuscleGroups.Any())
        {
            var muscleGroups = new List<DbMuscleGroup>
            {
                new DbMuscleGroup { Name = "Chest", Description = "Usually trained on upper body day. Or on separate. Not with back" },
                new DbMuscleGroup { Name = "Back", Description = "Usually trained on upper body day. Or on separate. Not with chest" },
                new DbMuscleGroup { Name = "Legs", Description = "Usually trained on leg day." },
                new DbMuscleGroup { Name = "Shoulders", Description = "Usually trained on upper body day with chest and triceps. Or on separate day." },
                new DbMuscleGroup { Name = "Arms", Description = "Biceps usually trained with backs, while triceps with chest and shoulders. Or on separate day" },
                new DbMuscleGroup { Name = "Abs", Description = "Abs are usually trained two - three day per week." }
            };

            await dbContext.MuscleGroups.AddRangeAsync(muscleGroups);
            await dbContext.SaveChangesAsync();
        }
    }
}
