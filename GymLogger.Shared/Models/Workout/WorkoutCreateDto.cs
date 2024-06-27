using GymLogger.Shared.Models.ExerciseWorkout;

namespace GymLogger.Shared.Models.Workout;
public class WorkoutCreateDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime? Date { get; set; } = DateTime.Now;
    public List<ExerciseWorkoutCreateDto> Exercises { get; set; } = [];
}
