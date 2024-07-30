using GymLogger.Core.Dashboards.Interfaces;

namespace GymLogger.Application.Dashboard;
internal class DashboardService(IDashboardRepository repository) : IDashboardService
{
    public Task<IDashboard?> GetAsync() => repository.GetAsync();
}
