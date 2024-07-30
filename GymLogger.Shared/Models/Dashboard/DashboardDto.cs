using GymLogger.Shared.Models.Workout;

namespace GymLogger.Shared.Models.Dashboard;
public class DashboardDto
{
    public WorkoutDto? LastWorkout { get; set; }
    public string? FavoriteMuscleGroupName { get; set; }
    public int WorkoutsCount { get; set; }
    public int WorkoutsThisWeek { get; set; }
    public int WorkoutsThisMonth { get; set; }
    public int WorkoutsThisYear { get; set; }
    public decimal? SeriesThisWeek { get; set; }
    public decimal? SeriesThisMonth { get; set; }
    public decimal? SeriesThisYear { get; set; }
    public decimal? WeightThisWeek { get; set; }
    public decimal? WeightThisMonth { get; set; }
    public decimal? WeightThisYear { get; set; }
    public ICollection<DashboardDateItemDto>? WorkoutsByDate { get; set; }
}
