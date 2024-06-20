using GymLogger.Common.Enums;
using GymLogger.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GymLogger.Shared.Models.Exercise;

public class ExerciseCreateDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [GuidNotEmpty(ErrorMessage = "Muscle Group ID is required.")]
    public Guid MuscleGroupId { get; set; }
    public string? Description { get; set; }

    [EnumValidation(typeof(ExerciseLogType), allowZero: false, ErrorMessage = "Select on of options")]
    public ExerciseLogType ExerciseLogType { get; set; }
}
