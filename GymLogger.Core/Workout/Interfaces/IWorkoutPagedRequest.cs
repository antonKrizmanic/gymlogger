using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Core.Workout.Interfaces;
public interface IWorkoutPagedRequest : IPagedRequest
{
    Guid MuscleGroupId { get; set; }
    DateTime? WorkoutDate { get; set; }
}
