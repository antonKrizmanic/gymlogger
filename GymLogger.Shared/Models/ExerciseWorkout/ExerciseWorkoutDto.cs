using GymLogger.Shared.Models.ExerciseSet;

namespace GymLogger.Shared.Models.ExerciseWorkout;
public class ExerciseWorkoutDto
{
    public Guid ExerciseId { get; set; }
    public Guid WorkoutId { get; set; }
    public decimal? TotalWeight { get; set; }
    public decimal? TotalReps { get; set; }
    public decimal? TotalSets { get; set; }
    public ICollection<ExerciseSetDto> Sets { get; set; }
}
