namespace GymLogger.Core.Exercise.Interfaces;
public interface IExerciseUpdate : IExerciseCreate
{
    Guid Id { get; set; }
}
