using GymLogger.Api.Client.Components;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.ExerciseWorkout;
using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Workouts.Components;

public partial class ExerciseWorkoutForm : BaseComponent
{
    [Parameter] public required ExerciseWorkoutCreateDto Model { get; set; }
    [Parameter] public EventCallback<ExerciseWorkoutCreateDto> OnSave { get; set; }
    [Parameter] public EventCallback OnClose { get; set; }
    //[Inject] private IExerciseApiService ExerciseApiService { get; set; } = default!;

    [Parameter] public required ICollection<ExerciseDto> Exercises { get; set; }


    private async Task SaveAsync()
    {
        await this.OnSave.InvokeAsync(this.Model);
    }

    private void Close()
    {
        this.OnClose.InvokeAsync();
    }


}
