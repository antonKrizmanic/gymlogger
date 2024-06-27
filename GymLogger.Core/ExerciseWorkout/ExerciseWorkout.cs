using GymLogger.Core.ExerciseWorkout.Interfaces;

namespace GymLogger.Core.ExerciseWorkout;

public class ExerciseWorkout : IExerciseWorkout
{
    public Guid ExerciseId { get; set; }
    public Guid WorkoutId { get; set; }
    public decimal? TotalWeight { get; set; }
    public decimal? TotalReps { get; set; }
    public decimal? TotalSets { get; set; }
}
