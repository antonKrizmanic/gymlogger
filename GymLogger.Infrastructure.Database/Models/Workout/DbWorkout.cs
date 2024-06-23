using GymLogger.Core.User.Interfaces;
using GymLogger.Infrastructure.Database.Models.MuscleGroups;

namespace GymLogger.Infrastructure.Database.Models.Workout;
public class DbWorkout : IBelongsToUser
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public Guid MuscleGroupId { get; set; }
    public virtual DbMuscleGroup MuscleGroup { get; set; } = default!;
    public decimal? TotalWeight { get; set; }
    public decimal? TotalReps { get; set; }
    public decimal? TotalSets { get; set; }
    public string? BelongsToUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
