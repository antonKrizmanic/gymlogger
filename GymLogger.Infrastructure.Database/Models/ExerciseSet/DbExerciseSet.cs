using GymLogger.Infrastructure.Database.Models.ExerciseWorkout;

namespace GymLogger.Infrastructure.Database.Models.ExerciseSet;
public class DbExerciseSet
{
    public Guid Id { get; set; }
    public Guid ExerciseWorkoutId { get; set; }
    public virtual DbExerciseWorkout ExerciseWorkout { get; set; } = default!;
    public required int Index { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Reps { get; set; }
    public decimal? Time { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
