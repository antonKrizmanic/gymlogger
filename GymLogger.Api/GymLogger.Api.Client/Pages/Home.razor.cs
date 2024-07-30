using GymLogger.Api.Client.Components;
using GymLogger.Shared.Models.Dashboard;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages;

public partial class Home : BaseComponent
{
    [Inject] private IDashboardApiService DashboardApiService { get; set; } = null!;
    private DashboardDto? Dashboard { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Dashboard = await DashboardApiService.GetAsync();
    }
}
