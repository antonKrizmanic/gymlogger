using GymLogger.Core.ExerciseWorkout.Interfaces;
using GymLogger.Core.Workout.Interfaces;

namespace GymLogger.Core.Workout;
public class WorkoutCreate : IWorkoutCreate
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public Guid MuscleGroupId { get; set; }
    public decimal? TotalWeight { get; set; }
    public decimal? TotalReps { get; set; }
    public decimal? TotalSets { get; set; }
    public ICollection<IExerciseWorkoutCreate>? Exercises { get; set; }
}
