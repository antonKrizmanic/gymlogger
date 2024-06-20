using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Models.Paging;
using System.Text.Json.Serialization;

namespace GymLogger.Shared.SourceGeneration;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, WriteIndented = true)]
[JsonSerializable(typeof(ExerciseCreateDto))]
[JsonSerializable(typeof(ExerciseDto))]
[JsonSerializable(typeof(ExerciseUpdateDto))]
[JsonSerializable(typeof(PagedResponseDto<ExerciseDto>))]

[JsonSerializable(typeof(MuscleGroupDto))]
[JsonSerializable(typeof(PagedResponseDto<MuscleGroupDto>))]
public partial class JsonSourceGenerationContext : JsonSerializerContext
{

}
