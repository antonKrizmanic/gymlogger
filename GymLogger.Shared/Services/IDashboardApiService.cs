using GymLogger.Shared.Models.Dashboard;

namespace GymLogger.Shared.Services
{
    public interface IDashboardApiService
    {
        Task<DashboardDto?> GetAsync();
    }
}