using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.ExerciseSet;

namespace GymLogger.Api.Client.Models.Workout;

public class ExerciseSetFormViewModel
{
    public ExerciseSetCreateDto CreateDto { get; set; }
    public ExerciseDto Exercise { get; set; }
}
