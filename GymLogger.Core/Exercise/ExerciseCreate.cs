using GymLogger.Common.Enums;
using GymLogger.Core.Exercise.Interfaces;

namespace GymLogger.Core.Exercise;
public class ExerciseCreate : IExerciseCreate
{
    public string Name { get; set; } = string.Empty;
    public Guid MuscleGroupId { get; set; }
    public string? Description { get; set; }
    public ExerciseLogType ExerciseLogType { get; set; }
    public bool IsPublic { get; set; }
}
