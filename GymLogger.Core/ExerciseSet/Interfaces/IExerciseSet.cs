namespace GymLogger.Core.ExerciseSet.Interfaces;
public interface IExerciseSet
{
    Guid Id { get; set; }
    Guid ExerciseWorkoutId { get; set; }
    int Index { get; set; }
    decimal? Weight { get; set; }
    decimal? Reps { get; set; }
    decimal? Time { get; set; }
}
