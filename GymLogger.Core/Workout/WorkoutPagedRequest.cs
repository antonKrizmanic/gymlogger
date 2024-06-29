using GymLogger.Core.Paging;
using GymLogger.Core.Workout.Interfaces;

namespace GymLogger.Core.Workout;
public class WorkoutPagedRequest : PagedRequest, IWorkoutPagedRequest
{
    public Guid MuscleGroupId { get; set; }
}
