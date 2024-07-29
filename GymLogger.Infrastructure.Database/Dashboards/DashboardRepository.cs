using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymLogger.Core.Dashboards;
using GymLogger.Core.Dashboards.Interfaces;
using GymLogger.Core.Workout.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymLogger.Infrastructure.Database.Dashboards;
internal class DashboardRepository(GymLoggerDbContext context, IMapper mapper, ILogger<DashboardRepository> logger) : IDashboardRepository
{
    public async Task<IDashboard?> GetAsync()
    {
        if (context.Workouts.Count() == 0)
        {
            return null;
        }

        var dashboard = new Dashboard();

        // Last workout
        var lastWorkout = await context.Workouts
            .AsNoTracking()
            .OrderByDescending(w => w.Date)
            .ProjectTo<IWorkout>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        dashboard.LastWorkout = lastWorkout;

        // Workouts count
        dashboard.WorkoutsCount = context.Workouts.Count();

        // Workouts this week
        var dayOfWeek = (int)DateTime.Today.DayOfWeek;
        var daysUntilMonday = (dayOfWeek + 6) % 7;
        var mondayDate = DateTime.Today.AddDays(-daysUntilMonday);

        dashboard.WorkoutsThisWeek = context.Workouts
            .Count(w => w.Date >= mondayDate);

        // Workouts this month
        var firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        dashboard.WorkoutsThisMonth = context.Workouts
            .Count(w => w.Date >= firstDayOfMonth);

        // Workouts this year
        var firstDayOfYear = new DateTime(DateTime.Today.Year, 1, 1);
        dashboard.WorkoutsThisYear = context.Workouts
            .Count(w => w.Date >= firstDayOfYear);

        // Series this week
        dashboard.SeriesThisWeek = context.ExerciseWorkouts
            .Where(s => s.Workout.Date >= mondayDate)
            .Sum(s => s.TotalSets);

        // Series this month
        dashboard.SeriesThisMonth = context.ExerciseWorkouts
            .Where(s => s.Workout.Date >= firstDayOfMonth)
            .Sum(s => s.TotalSets);

        // Series this year
        dashboard.SeriesThisYear = context.ExerciseWorkouts
            .Where(s => s.Workout.Date >= firstDayOfYear)
            .Sum(s => s.TotalSets);

        // Weight this week
        dashboard.WeightThisWeek = context.ExerciseSets
            .Where(s => s.ExerciseWorkout.Workout.Date >= mondayDate)
            .Sum(s => s.Weight * s.Reps);

        // Weight this month
        dashboard.WeightThisMonth = context.ExerciseSets
            .Where(s => s.ExerciseWorkout.Workout.Date >= firstDayOfMonth)
            .Sum(s => s.Weight * s.Reps);

        // Weight this year
        dashboard.WeightThisYear = context.ExerciseSets
            .Where(s => s.ExerciseWorkout.Workout.Date >= firstDayOfYear)
            .Sum(s => s.Weight * s.Reps);

        // WorkoutsByDate (for current month)
        // Foreach day in this month create a DashboardDateItem
        var workoutsByDate = new List<IDashboardDateItem>();
        for (var i = 1; i <= DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month); i++)
        {
            var currentDate = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, i);
            var workoutsForDate = context.Workouts
                .Where(w => w.Date == currentDate.ToDateTime(new TimeOnly(0, 0, 0)))
                .SelectMany(w => w.Exercises)
                .SelectMany(ew => ew.Sets)
                .ToList();

            var weight = workoutsForDate.Sum(s => s.Weight * s.Reps);
            var series = workoutsForDate.Sum(s => s.Reps);
            workoutsByDate.Add(new DashboardDateItem
            {
                Date = currentDate,
                Weight = weight,
                Series = series,
                Reps = workoutsForDate.Count
            });
        }

        dashboard.WorkoutsByDate = workoutsByDate;

        return dashboard;
    }
}
