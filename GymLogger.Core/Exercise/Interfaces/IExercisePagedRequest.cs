using GymLogger.Common.Enums;
using GymLogger.Core.Paging.Interfaces;

namespace GymLogger.Core.Exercise.Interfaces;

public interface IExercisePagedRequest : IPagedRequest
{
    Guid MuscleGroupId { get; set; }
    ExerciseLogType ExerciseLogType { get; set; }
}
