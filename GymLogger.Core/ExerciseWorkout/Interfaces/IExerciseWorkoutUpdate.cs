namespace GymLogger.Core.ExerciseWorkout.Interfaces;
public interface IExerciseWorkoutUpdate : IExerciseWorkoutCreate
{
    Guid WorkoutId { get; set; }
}
