﻿using GymLogger.Core.ExerciseSet.Interfaces;

namespace GymLogger.Core.ExerciseWorkout.Interfaces;
public interface IExerciseWorkoutCreate
{
    Guid ExerciseId { get; set; }
    decimal? TotalWeight { get; set; }
    decimal? TotalReps { get; set; }
    decimal? TotalSets { get; set; }
    string? Note { get; set; }
    int Index { get; set; }
    ICollection<IExerciseSetCreate>? Sets { get; set; }
}
