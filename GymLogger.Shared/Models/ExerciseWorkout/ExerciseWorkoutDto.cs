using GymLogger.Common.Enums;
using GymLogger.Shared.Models.ExerciseSet;

namespace GymLogger.Shared.Models.ExerciseWorkout;
public class ExerciseWorkoutDto
{
    public Guid ExerciseId { get; set; }
    public string? ExerciseName { get; set; }
    public Guid WorkoutId { get; set; }
    public decimal? TotalWeight { get; set; }
    public decimal? TotalReps { get; set; }
    public decimal? TotalSets { get; set; }
    public string? Note { get; set; }
    public int Index { get; set; }
    public ExerciseLogType ExerciseLogType { get; set; }
    public ICollection<ExerciseSetDto> Sets { get; set; } = [];
}

public class ExerciseWorkoutDetailDto : ExerciseWorkoutDto
{
    public DateTime WorkoutDate { get; set; }
}
