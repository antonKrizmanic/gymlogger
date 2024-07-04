namespace GymLogger.Core.ExerciseSet.Interfaces;
public interface IExerciseSetCreate
{
    int Index { get; set; }
    decimal? Weight { get; set; }
    decimal? Reps { get; set; }
    decimal? Time { get; set; }
    string? Note { get; set; }
}
