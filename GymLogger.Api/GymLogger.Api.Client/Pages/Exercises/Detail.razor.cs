using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Exercises;

public partial class Detail : ComponentBase
{
    [Parameter] public Guid Id { get; set; }
    [Inject] private IExerciseApiService ExerciseApiService { get; set; }
    private ExerciseDto Exercise { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        this.Exercise = await ExerciseApiService.GetAsync(Id);
    }
}
