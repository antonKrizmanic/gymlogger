using GymLogger.Shared.Models.Exercise;
using System.Text.Json.Serialization;

namespace GymLogger.Shared.SourceGeneration;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, WriteIndented = true)]
[JsonSerializable(typeof(ExerciseCreateDto))]
[JsonSerializable(typeof(ExerciseDto))]
[JsonSerializable(typeof(ExerciseUpdateDto))]
public partial class JsonSourceGenerationContext : JsonSerializerContext
{

}
