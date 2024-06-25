using GymLogger.Core.ExerciseWorkout.Interfaces;

namespace GymLogger.Core.ExerciseWorkout;
internal class ExerciseWorkoutCreate : IExerciseWorkoutCreate
{
    public Guid ExerciseId { get; set; }
    public decimal? TotalWeight { get; set; }
    public decimal? TotalReps { get; set; }
    public decimal? TotalSets { get; set; }
}
