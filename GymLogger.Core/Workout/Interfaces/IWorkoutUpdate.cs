namespace GymLogger.Core.Workout.Interfaces;
public interface IWorkoutUpdate : IWorkoutCreate
{
    Guid Id { get; set; }
}
