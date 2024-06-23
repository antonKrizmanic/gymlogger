namespace GymLogger.Core.Workout.Interfaces;
public interface IWorkoutCreate
{
    string Name { get; set; }
    string? Description { get; set; }
    DateTime Date { get; set; }
    Guid MuscleGroupId { get; set; }
    decimal? TotalWeight { get; set; }
    decimal? TotalReps { get; set; }
    decimal? TotalSets { get; set; }
    // TODO: List of exercises (optional, workout can be updated with them)
}
