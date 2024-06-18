using GymLogger.Common.Enums;
using GymLogger.Infrastructure.Database.Models.MuscleGroups;
using System.ComponentModel.DataAnnotations;

namespace GymLogger.Infrastructure.Database.Models.Exercise;
public class DbExercise
{
    public Guid Id { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;
    public Guid MuscleGroupId { get; set; }
    public virtual DbMuscleGroup MuscleGroup { get; set; }
    public ExerciseLogType ExerciseLogType { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
