namespace GymLogger.Shared.Models.Dashboard;
public class DashboardDateItemDto
{
    public DateOnly Date { get; set; }
    public decimal? Weight { get; set; }
    public string WeightString => Weight?.ToString("0.##");
    public decimal? Series { get; set; }
    public decimal? Reps { get; set; }
}
