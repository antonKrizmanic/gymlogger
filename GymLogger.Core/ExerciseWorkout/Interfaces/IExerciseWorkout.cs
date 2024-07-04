﻿using GymLogger.Core.ExerciseSet.Interfaces;

namespace GymLogger.Core.ExerciseWorkout.Interfaces;

public interface IExerciseWorkout
{
    Guid ExerciseId { get; set; }
    string ExerciseName { get; set; }
    Guid WorkoutId { get; set; }
    decimal? TotalWeight { get; set; }
    decimal? TotalReps { get; set; }
    decimal? TotalSets { get; set; }
    string? Note { get; set; }
    ICollection<IExerciseSet> Sets { get; set; }
}