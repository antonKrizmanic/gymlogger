﻿using GymLogger.Core.Workout.Interfaces;

namespace GymLogger.Core.Workout;
public class Workout : IWorkout
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public string? BelongsToUserId { get; set; }
    public Guid MuscleGroupId { get; set; }
    public string MuscleGroupName { get; set; }
    public decimal? TotalWeight { get; set; }
    public decimal? TotalReps { get; set; }
    public decimal? TotalSets { get; set; }
}
