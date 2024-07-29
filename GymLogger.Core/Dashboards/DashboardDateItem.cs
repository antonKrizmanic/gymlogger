using GymLogger.Core.Dashboards.Interfaces;

namespace GymLogger.Core.Dashboards;
public class DashboardDateItem : IDashboardDateItem
{
    public DateOnly Date { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Series { get; set; }
    public decimal? Reps { get; set; }
}
