using GymLogger.Core.Exercise.Interfaces;

namespace GymLogger.Core.Exercise;
public class ExerciseUpdate : ExerciseCreate, IExerciseUpdate
{
    public Guid Id { get; set; }
}
