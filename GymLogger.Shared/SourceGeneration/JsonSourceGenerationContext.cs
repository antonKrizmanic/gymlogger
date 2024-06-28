using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Models.Workout;
using System.Text.Json.Serialization;

namespace GymLogger.Shared.SourceGeneration;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, WriteIndented = true)]
[JsonSerializable(typeof(ExerciseCreateDto))]
[JsonSerializable(typeof(ExerciseDto))]
[JsonSerializable(typeof(ExerciseUpdateDto))]
[JsonSerializable(typeof(PagedResponseDto<ExerciseDto>))]

[JsonSerializable(typeof(WorkoutDetailsDto))]
[JsonSerializable(typeof(WorkoutCreateDto))]
[JsonSerializable(typeof(WorkoutDto))]
[JsonSerializable(typeof(WorkoutUpdateDto))]
[JsonSerializable(typeof(PagedResponseDto<WorkoutDto>))]

[JsonSerializable(typeof(MuscleGroupDto))]
[JsonSerializable(typeof(PagedResponseDto<MuscleGroupDto>))]
public partial class JsonSourceGenerationContext : JsonSerializerContext
{

}
