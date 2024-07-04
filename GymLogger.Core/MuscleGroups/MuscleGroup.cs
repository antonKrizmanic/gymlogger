﻿using GymLogger.Core.MuscleGroups.Interfaces;

namespace GymLogger.Core.MuscleGroups;
public class MuscleGroup : IMuscleGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}