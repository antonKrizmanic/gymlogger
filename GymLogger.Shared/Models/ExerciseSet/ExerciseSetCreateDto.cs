namespace GymLogger.Shared.Models.ExerciseSet;
public class ExerciseSetCreateDto
{
    public decimal? Reps { get; set; } = default!;
    public decimal? Weight { get; set; } = default!;
    public decimal? Time { get; set; } = default!;
}
