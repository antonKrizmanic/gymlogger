using GymLogger.Shared.Models.Paging;

namespace GymLogger.Shared.Models.Workout;
public class WorkoutPagedRequestDto : PagedRequestDto
{
    public Guid MuscleGroupId { get; set; }
    public DateTime? WorkoutDate { get; set; }
}
