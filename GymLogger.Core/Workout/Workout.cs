using GymLogger.Core.Workout.Interfaces;

namespace GymLogger.Core.Workout;
public class Workout : IWorkout
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public string? BelongsToUserId { get; set; }
    public Guid MuscleGroupId { get; set; }
    public decimal? TotalWeight { get; set; }
    public decimal? TotalReps { get; set; }
    public decimal? TotalSets { get; set; }
}
