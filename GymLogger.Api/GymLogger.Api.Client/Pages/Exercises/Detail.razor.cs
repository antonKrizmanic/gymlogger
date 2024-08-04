using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.ExerciseWorkout;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Exercises;

public partial class Detail : ComponentBase
{
    [Parameter] public Guid Id { get; set; }
    [Inject] private IExerciseApiService ExerciseApiService { get; set; }
    [Inject] private IExerciseWorkoutApiService ExerciseWorkoutApiService { get; set; }
    private ExerciseDto Exercise { get; set; } = default!;
    private ICollection<ExerciseWorkoutDetailDto> ExerciseWorkouts { get; set; } = [];
    protected override async Task OnInitializedAsync()
    {
        this.Exercise = await ExerciseApiService.GetAsync(Id);

        var result = await this.ExerciseWorkoutApiService.GetPagedListAsync(new()
        {
            ExerciseId = this.Id,
            PageSize = int.MaxValue,
        });

        this.ExerciseWorkouts = result.Items;
    }
}
