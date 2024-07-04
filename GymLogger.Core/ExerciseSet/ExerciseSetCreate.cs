using GymLogger.Core.ExerciseSet.Interfaces;

namespace GymLogger.Core.ExerciseSet;
public class ExerciseSetCreate : IExerciseSetCreate
{
    public required int Index { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Reps { get; set; }
    public decimal? Time { get; set; }
    public string? Note { get; set; }
}
