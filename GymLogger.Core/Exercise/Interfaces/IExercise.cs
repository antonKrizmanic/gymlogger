using GymLogger.Common.Enums;

namespace GymLogger.Core.Exercise.Interfaces;
public interface IExercise
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid MuscleGroupId { get; set; }
    public string MuscleGroupName { get; set; }
    public string? Description { get; set; }
    public ExerciseLogType ExerciseLogType { get; set; }
    public string? BelongsToUserId { get; set; }
}
