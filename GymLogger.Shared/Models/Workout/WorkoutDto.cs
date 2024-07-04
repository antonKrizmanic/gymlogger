namespace GymLogger.Shared.Models.Workout;
public record WorkoutDto(
    Guid Id,
    string Name,
    string? Description,
    DateTime Date,
    Guid MuscleGroupId,
    string MuscleGroupName,
    decimal? TotalWeight,
    decimal? TotalReps,
    decimal? TotalSets);

