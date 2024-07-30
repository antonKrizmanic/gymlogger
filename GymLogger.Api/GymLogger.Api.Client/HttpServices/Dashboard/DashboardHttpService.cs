using GymLogger.Shared.Constants;
using GymLogger.Shared.Models.Dashboard;
using GymLogger.Shared.Services;
using System.Net.Http.Json;

namespace GymLogger.Api.Client.HttpServices.Dashboard;

public class DashboardHttpService(IHttpClientFactory httpClientFactory) : BaseHttpService(httpClientFactory, ApiRoutes.Dashboard), IDashboardApiService
{
    public async Task<DashboardDto?> GetAsync()
    {
        return await base.HttpClient.GetFromJsonAsync($"{base.ApiRoute}", Context.DashboardDto);
    }
}
