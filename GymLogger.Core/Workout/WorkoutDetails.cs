using GymLogger.Core.ExerciseWorkout.Interfaces;
using GymLogger.Core.Workout.Interfaces;

namespace GymLogger.Core.Workout;
public class WorkoutDetails : Workout, IWorkoutDetails
{
    public ICollection<IExerciseWorkout> Exercises { get; set; }
}
