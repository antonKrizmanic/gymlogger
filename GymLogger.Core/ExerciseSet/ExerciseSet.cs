using GymLogger.Core.ExerciseSet.Interfaces;

namespace GymLogger.Core.ExerciseSet;
public class ExerciseSet : IExerciseSet
{
    public Guid Id { get; set; }
    public Guid ExerciseWorkoutId { get; set; }
    public int Index { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Reps { get; set; }
    public decimal? Time { get; set; }
}
