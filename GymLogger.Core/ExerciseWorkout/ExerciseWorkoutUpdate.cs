using GymLogger.Core.ExerciseWorkout.Interfaces;

namespace GymLogger.Core.ExerciseWorkout;
internal class ExerciseWorkoutUpdate : IExerciseWorkoutUpdate
{
    public Guid WorkoutId { get; set; }
    public Guid ExerciseId { get; set; }
    public decimal? TotalWeight { get; set; }
    public decimal? TotalReps { get; set; }
    public decimal? TotalSets { get; set; }
}
