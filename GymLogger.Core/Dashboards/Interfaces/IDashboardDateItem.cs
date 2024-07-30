namespace GymLogger.Core.Dashboards.Interfaces;
public interface IDashboardDateItem
{
    DateOnly Date { get; set; }
    decimal? Weight { get; set; }
    decimal? Series { get; set; }
    decimal? Reps { get; set; }
}
