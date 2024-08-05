using GymLogger.Core.ExerciseSet.Interfaces;
using GymLogger.Core.ExerciseWorkout.Interfaces;

namespace GymLogger.Core.ExerciseWorkout;

public class ExerciseWorkout : IExerciseWorkout
{
    public Guid ExerciseId { get; set; }
    public string? ExerciseName { get; set; }
    public Guid WorkoutId { get; set; }
    public decimal? TotalWeight { get; set; }
    public decimal? TotalReps { get; set; }
    public decimal? TotalSets { get; set; }
    public string? Note { get; set; }
    public int Index { get; set; }
    public ICollection<IExerciseSet>? Sets { get; set; }
}

public class ExerciseWorkoutDetail : ExerciseWorkout, IExerciseWorkoutDetail
{
    public DateTime WorkoutDate { get; set; }
}