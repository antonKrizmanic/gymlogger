using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Core.ExerciseWorkout.Interfaces;
public interface IExerciseWorkoutPagedRequest : IPagedRequest
{
    Guid ExerciseId { get; set; }
    Guid WorkoutId { get; set; }
}
