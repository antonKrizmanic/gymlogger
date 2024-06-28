using GymLogger.Shared.Models.ExerciseWorkout;

namespace GymLogger.Shared.Models.Workout;
public record WorkoutDetailsDto(
    Guid Id,
    string Name,
    string? Description,
    DateTime Date,
    Guid MuscleGroupId,
    decimal? TotalWeight,
    decimal? TotalReps,
    decimal? TotalSets,
    ICollection<ExerciseWorkoutDto> Exercises)
    : WorkoutDto(Id, Name, Description, Date, MuscleGroupId, TotalWeight, TotalReps, TotalSets)
{
    public ICollection<ExerciseWorkoutDto> Exercises { get; set; } = Exercises;
}
