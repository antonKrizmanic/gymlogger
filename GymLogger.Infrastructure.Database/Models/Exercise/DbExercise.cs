using GymLogger.Common.Enums;
using GymLogger.Core.User.Interfaces;
using GymLogger.Infrastructure.Database.Models.MuscleGroups;
using System.ComponentModel.DataAnnotations;

namespace GymLogger.Infrastructure.Database.Models.Exercise;
public class DbExercise : IBelongsToUser
{
    public Guid Id { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;
    public Guid MuscleGroupId { get; set; }
    public virtual DbMuscleGroup MuscleGroup { get; set; } = default!;
    public ExerciseLogType ExerciseLogType { get; set; }
    public string? Description { get; set; }
    public string? BelongsToUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
