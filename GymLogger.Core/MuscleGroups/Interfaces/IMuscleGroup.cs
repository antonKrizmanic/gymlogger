namespace GymLogger.Core.MuscleGroups.Interfaces;
public interface IMuscleGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
