using GymLogger.Shared.Models.ExerciseWorkout;

namespace GymLogger.Shared.Models.Workout;
public record WorkoutDetailsDto : WorkoutDto
{
    public ICollection<ExerciseWorkoutDto> Exercises { get; set; }
    public WorkoutDetailsDto(
        Guid Id,
        string Name,
        string? Description,
        DateTime Date,
        Guid MuscleGroupId,
        decimal? TotalWeight,
        decimal? TotalReps,
        decimal? TotalSets, ICollection<ExerciseWorkoutDto> Exercises) : base(Id, Name, Description, Date, MuscleGroupId, TotalWeight, TotalReps, TotalSets)
    {
        this.Exercises = Exercises;
    }
}
