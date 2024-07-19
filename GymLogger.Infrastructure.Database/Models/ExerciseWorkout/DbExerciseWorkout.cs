using GymLogger.Infrastructure.Database.Models.Exercise;
using GymLogger.Infrastructure.Database.Models.ExerciseSet;
using GymLogger.Infrastructure.Database.Models.Workout;

namespace GymLogger.Infrastructure.Database.Models.ExerciseWorkout;
public class DbExerciseWorkout
{
    public Guid Id { get; set; }
    public Guid WorkoutId { get; set; }
    public virtual DbWorkout Workout { get; set; } = default!;
    public Guid ExerciseId { get; set; }
    public virtual DbExercise Exercise { get; set; } = default!;
    public decimal? TotalWeight { get; set; }
    public decimal? TotalReps { get; set; }
    public decimal? TotalSets { get; set; }
    public string? Note { get; set; }
    public int Index { get; set; }
    public string? BelongsTo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<DbExerciseSet>? Sets { get; set; }
}
