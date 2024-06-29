using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.ExerciseSet;

namespace GymLogger.Api.Client.Models.Workout;

public class ExerciseSetCreateFormViewModel
{
    public List<ExerciseSetCreateDto> Sets { get; set; } = [];
    public ExerciseDto Exercise { get; set; }
}
