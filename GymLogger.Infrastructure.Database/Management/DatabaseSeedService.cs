using GymLogger.Core.Management.Interfaces;
using GymLogger.Infrastructure.Database.Models.Exercise;
using GymLogger.Infrastructure.Database.Models.MuscleGroups;
using Microsoft.Extensions.Logging;

namespace GymLogger.Infrastructure.Database.Management;
internal class DatabaseSeedService(GymLoggerDbContext dbContext, ILogger<DatabaseSeedService> logger) : IDatabaseSeedService
{
    public async Task SeedDatabaseAsync()
    {
        try
        {
            await this.SeedMuscleGroupsAsync();
            await this.SeedExercisesAsync();
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

    private async Task SeedExercisesAsync()
    {
        // Get each muscle group and seed exercises for it
        var abs = dbContext.MuscleGroups.FirstOrDefault(m => m.Name == "Abs");
        var arms = dbContext.MuscleGroups.FirstOrDefault(m => m.Name == "Arms");
        var back = dbContext.MuscleGroups.FirstOrDefault(m => m.Name == "Back");
        var chest = dbContext.MuscleGroups.FirstOrDefault(m => m.Name == "Chest");
        var legs = dbContext.MuscleGroups.FirstOrDefault(m => m.Name == "Legs");
        var shoulders = dbContext.MuscleGroups.FirstOrDefault(m => m.Name == "Shoulders");

        // Abs
        await CreateExerciseIfDoesntExistAsync(abs, "Crunches", "Crunches are a classic core-strengthening exercise.");
        await CreateExerciseIfDoesntExistAsync(abs, "Leg Raises", "Leg raises are a great way to target the lower abs.");
        await CreateExerciseIfDoesntExistAsync(abs, "Russian Twists", "Russian twists are a great way to target the obliques.");

        // Arms
        await CreateExerciseIfDoesntExistAsync(arms, "Close Grip Bench Press", "3x6-10");
        await CreateExerciseIfDoesntExistAsync(arms, "Hammer Curl", "3x6-10");
        await CreateExerciseIfDoesntExistAsync(arms, "Triceps Pushdown (Rope)", "3x10-15");
        await CreateExerciseIfDoesntExistAsync(arms, "Rope Cable Curls", "3x6-10");
        await CreateExerciseIfDoesntExistAsync(arms, "Overhead Triceps Extension", "3x10-15");
        await CreateExerciseIfDoesntExistAsync(arms, "Bicep Curls", "3x6-10");

        // Back
        await CreateExerciseIfDoesntExistAsync(back, "Barbell Bent Over Row", "3x6-10");
        await CreateExerciseIfDoesntExistAsync(back, "Pull-Up", "3x6-10");
        await CreateExerciseIfDoesntExistAsync(back, "Lat Pulldown", "3x10-15");
        await CreateExerciseIfDoesntExistAsync(back, "Seated Cable Rows", "3x8-12");

        // Chest
        await CreateExerciseIfDoesntExistAsync(chest, "Bench Press", "3x5-8");
        await CreateExerciseIfDoesntExistAsync(chest, "Chin Up", "3x6-10");
        await CreateExerciseIfDoesntExistAsync(chest, "Incline machine press", "3x8-12");
        await CreateExerciseIfDoesntExistAsync(chest, "Cable Fly", "2x10-15");
        await CreateExerciseIfDoesntExistAsync(chest, "Decline press", "3x6-10");
        await CreateExerciseIfDoesntExistAsync(chest, "PacMac", "3x6-10");

        // Leg
        await CreateExerciseIfDoesntExistAsync(legs, "Deadlift", "3x5-8");
        await CreateExerciseIfDoesntExistAsync(legs, "Good morning", "2x10");
        await CreateExerciseIfDoesntExistAsync(legs, "Squat", "3x8");
        await CreateExerciseIfDoesntExistAsync(legs, "Leg Press", "3x8-12");
        await CreateExerciseIfDoesntExistAsync(legs, "Leg Curl", "3x10-15");
        await CreateExerciseIfDoesntExistAsync(legs, "Leg Extension", "3x10-15");
        await CreateExerciseIfDoesntExistAsync(legs, "Hip Abduction", "3x10-15");
        await CreateExerciseIfDoesntExistAsync(legs, "Hip Adduction", "3x10-15");

        // Shoulder
        await CreateExerciseIfDoesntExistAsync(shoulders, "External/Internal rotation", "2x10");
        await CreateExerciseIfDoesntExistAsync(shoulders, "Overhead Press", "3x5-8");
        await CreateExerciseIfDoesntExistAsync(shoulders, "Upright Rows", "4x8-12");
        await CreateExerciseIfDoesntExistAsync(shoulders, "Lateral Raises", "4x8-12");
        await CreateExerciseIfDoesntExistAsync(shoulders, "Cable Reverse Fly", "4x10-15");
        await CreateExerciseIfDoesntExistAsync(shoulders, "Cable Face pull", "3x8-12");
        await CreateExerciseIfDoesntExistAsync(shoulders, "Seated Calf Raise", "3x6-10");
    }

    private async Task CreateExerciseIfDoesntExistAsync(DbMuscleGroup muscleGroup, string name, string description)
    {
        var exercise = dbContext.Exercises.FirstOrDefault(e => e.Name == name);

        if (exercise == null)
        {
            exercise = new DbExercise
            {
                Name = name,
                Description = description,
                MuscleGroup = muscleGroup,
                ExerciseLogType = Common.Enums.ExerciseLogType.Weight,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await dbContext.Exercises.AddAsync(exercise);
            await dbContext.SaveChangesAsync();
        }
    }
}
