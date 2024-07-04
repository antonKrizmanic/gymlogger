using System.ComponentModel.DataAnnotations;

namespace GymLogger.Infrastructure.Database.Models.MuscleGroups;
public class DbMuscleGroup
{
    public Guid Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
