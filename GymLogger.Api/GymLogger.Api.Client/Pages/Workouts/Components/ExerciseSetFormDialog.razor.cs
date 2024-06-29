using GymLogger.Api.Client.Models.Workout;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Workouts.Components;

public partial class ExerciseSetFormDialog
{
    [Parameter] public ExerciseSetFormViewModel Content { get; set; }

    [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;

    private async Task SaveAsync()
    {
        await Dialog.CloseAsync(Content);
    }

    private async Task CancelAsync()
    {
        await Dialog.CancelAsync();
    }
}
