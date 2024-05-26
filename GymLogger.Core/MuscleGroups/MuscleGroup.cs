using GymLogger.Core.MuscleGroups.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Core.MuscleGroups;
public class MuscleGroup : IMuscleGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
