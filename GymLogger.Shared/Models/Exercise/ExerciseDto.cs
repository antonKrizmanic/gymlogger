using GymLogger.Common.Enums;

namespace GymLogger.Shared.Models.Exercise;
public record ExerciseDto(
    Guid Id,
    string Name,
    Guid MuscleGroupId,
    string MuscleGroupName,
    string? Description,
    ExerciseLogType ExerciseLogType);
