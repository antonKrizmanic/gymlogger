using GymLogger.Shared.Models.Paging;

namespace GymLogger.Shared.Models.ExerciseWorkout;

public class ExerciseWorkoutPagedRequestDto : PagedRequestDto
{
    public Guid? ExerciseId { get; set; }
    public Guid? WorkoutId { get; set; }
}
