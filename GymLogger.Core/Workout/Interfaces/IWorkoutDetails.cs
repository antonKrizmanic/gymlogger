using GymLogger.Core.ExerciseWorkout.Interfaces;

namespace GymLogger.Core.Workout.Interfaces;
public interface IWorkoutDetails : IWorkout
{
    ICollection<IExerciseWorkout> Exercises { get; set; }
}
