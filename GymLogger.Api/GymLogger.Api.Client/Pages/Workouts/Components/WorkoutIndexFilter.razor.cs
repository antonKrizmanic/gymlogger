using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Models.Workout;
using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Workouts.Components;

public partial class WorkoutIndexFilter : ComponentBase
{
    [Parameter] public required WorkoutPagedRequestDto PagedRequestDto { get; set; }
    [Parameter] public required ICollection<MuscleGroupDto> MuscleGroups { get; set; }
    [Parameter] public EventCallback<WorkoutPagedRequestDto> OnFilter { get; set; }

    

    private string SelectedMuscleGroupId
    {
        get => PagedRequestDto.MuscleGroupId.ToString();
        set => PagedRequestDto.MuscleGroupId = string.IsNullOrEmpty(value) ? Guid.Empty : Guid.Parse(value);
    }

    private void ClearFilter()
    {
        this.SelectedMuscleGroupId = Guid.Empty.ToString();
        this.PagedRequestDto.WorkoutDate = null;
    }
}