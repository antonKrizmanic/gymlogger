using GymLogger.Common.Enums;
using GymLogger.Shared.Models.Paging;

namespace GymLogger.Shared.Models.Exercise;
public class ExercisePagedRequestDto : PagedRequestDto
{
    public Guid MuscleGroupId { get; set; }
    public ExerciseLogType ExerciseLogType { get; set; }
}
