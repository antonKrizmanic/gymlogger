namespace GymLogger.Core.Dashboards.Interfaces;
public interface IDashboardService
{
    Task<IDashboard?> GetAsync();
}
