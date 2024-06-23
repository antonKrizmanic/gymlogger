using GymLogger.Core.Workout.Interfaces;

namespace GymLogger.Core.Workout;
public class WorkoutCreate : IWorkoutCreate
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
}
