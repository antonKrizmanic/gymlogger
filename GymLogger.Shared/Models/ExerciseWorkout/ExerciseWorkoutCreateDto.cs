﻿using GymLogger.Shared.Models.ExerciseSet;

namespace GymLogger.Shared.Models.ExerciseWorkout;
public class ExerciseWorkoutCreateDto
{
    public string ExerciseId { get; set; }
    public int Index { get; set; } 
    public string? Note { get; set; }
    public ICollection<ExerciseSetCreateDto> Sets { get; set; } = [];
}
