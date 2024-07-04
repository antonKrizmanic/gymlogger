using GymLogger.Common.Enums;
using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.Paging;

namespace GymLogger.Core.Exercise;
public class ExercisePagedRequest : PagedRequest, IExercisePagedRequest
{
    public Guid MuscleGroupId { get; set; }
    public ExerciseLogType ExerciseLogType { get; set; }
}
