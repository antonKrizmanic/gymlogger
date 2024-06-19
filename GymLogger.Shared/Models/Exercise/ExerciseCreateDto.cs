using GymLogger.Common.Enums;

namespace GymLogger.Shared.Models.Exercise;

public class ExerciseCreateDto
{
    public string Name { get; set; } = string.Empty;
    public Guid MuscleGroupId { get; set; }
    public string? Description { get; set; }
    public ExerciseLogType ExerciseLogType { get; set; }
}
