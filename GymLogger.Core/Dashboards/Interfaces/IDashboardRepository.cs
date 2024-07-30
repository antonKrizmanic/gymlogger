namespace GymLogger.Core.Dashboards.Interfaces;
public interface IDashboardRepository
{
    Task<IDashboard?> GetAsync();
}
