using GymLogger.Common.Enums;
using GymLogger.Core.Exercise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Core.Exercise;
public class Exercise : IExercise
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid MuscleGroupId { get; set; }
    public string MuscleGroupName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ExerciseLogType ExerciseLogType { get; set; }
}
