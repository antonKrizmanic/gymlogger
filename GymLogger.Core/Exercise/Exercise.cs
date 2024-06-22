using GymLogger.Common.Enums;
using GymLogger.Core.Exercise.Interfaces;

namespace GymLogger.Core.Exercise;
public class Exercise : IExercise
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid MuscleGroupId { get; set; }
    public string MuscleGroupName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ExerciseLogType ExerciseLogType { get; set; }
    public string? BelongsToUserId { get; set; }
}
