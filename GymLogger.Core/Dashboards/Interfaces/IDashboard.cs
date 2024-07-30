using GymLogger.Core.Workout.Interfaces;

namespace GymLogger.Core.Dashboards.Interfaces;
public interface IDashboard
{
    IWorkout? LastWorkout { get; set; }
    string? FavoriteMuscleGroupName { get; set; }
    int WorkoutsCount { get; set; }
    int WorkoutsThisWeek { get; set; }
    int WorkoutsThisMonth { get; set; }
    int WorkoutsThisYear { get; set; }
    decimal? SeriesThisWeek { get; set; }
    decimal? SeriesThisMonth { get; set; }
    decimal? SeriesThisYear { get; set; }
    decimal? WeightThisWeek { get; set; }
    decimal? WeightThisMonth { get; set; }
    decimal? WeightThisYear { get; set; }
    ICollection<IDashboardDateItem>? WorkoutsByDate { get; set; }
}
