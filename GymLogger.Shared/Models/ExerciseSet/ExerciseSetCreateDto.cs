namespace GymLogger.Shared.Models.ExerciseSet;
public class ExerciseSetCreateDto
{
    public Guid Id { get; set; }
    public int Index { get; set; }
    public decimal? Reps { get; set; } = default!;
    public decimal? Weight { get; set; } = default!;
    public decimal? Time { get; set; } = default!;
}
