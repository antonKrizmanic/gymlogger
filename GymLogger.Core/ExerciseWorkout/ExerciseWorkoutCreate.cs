using GymLogger.Core.ExerciseSet.Interfaces;
using GymLogger.Core.ExerciseWorkout.Interfaces;

namespace GymLogger.Core.ExerciseWorkout;
public class ExerciseWorkoutCreate : IExerciseWorkoutCreate
{
    public Guid ExerciseId { get; set; }
    public decimal? TotalWeight { get; set; }
    public decimal? TotalReps { get; set; }
    public decimal? TotalSets { get; set; }
    public string? Note { get; set; }
    public ICollection<IExerciseSetCreate> Sets { get; set; }
}
