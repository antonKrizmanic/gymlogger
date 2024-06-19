using GymLogger.Common.Enums;

namespace GymLogger.Core.Exercise.Interfaces;

public interface IExerciseCreate
{
    string Name { get; set; }
    Guid MuscleGroupId { get; set; }
    string? Description { get; set; }
    ExerciseLogType ExerciseLogType { get; set; }
}
