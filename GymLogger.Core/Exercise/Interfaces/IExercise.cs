using GymLogger.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Core.Exercise.Interfaces;
public interface IExercise
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid MuscleGroupId { get; set; }
    public string MuscleGroupName { get; set; }
    public string? Description { get; set; }
    public ExerciseLogType ExerciseLogType { get; set; }
}
