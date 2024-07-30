using AutoMapper;
using GymLogger.Core.Dashboards.Interfaces;
using GymLogger.Shared.Models.Dashboard;
using GymLogger.Shared.Services;

namespace GymLogger.Api.Services.Dashboard;

public class DashboardApiService(IDashboardService service, IMapper mapper) : IDashboardApiService
{
    public async Task<DashboardDto?> GetAsync()
    {
        var dashboard = await service.GetAsync();
        return mapper.Map<DashboardDto>(dashboard);
    }
}
