using GymLogger.Shared.Models.ExerciseSet;

namespace GymLogger.Api.Client.Models.Workout;

public record EditSetEventArgs(Guid ExerciseId, ExerciseSetCreateDto Set);