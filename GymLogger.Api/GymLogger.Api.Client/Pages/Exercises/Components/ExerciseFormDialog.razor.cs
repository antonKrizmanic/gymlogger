using GymLogger.Common.Enums;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Exercises.Components;

public partial class ExerciseFormDialog
{
    [Inject] private IMuscleGroupApiService MuscleGroupHttpService { get; set; } = default!;
    [Parameter] public ExerciseCreateDto Content { get; set; }

    [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;

    private ICollection<MuscleGroupDto> MuscleGroups = new List<MuscleGroupDto>();

    private string selectedExerciseLogType
    {
        get => Content.ExerciseLogType.ToString();
        set => Content.ExerciseLogType = Enum.Parse<ExerciseLogType>(value);
    }

    private string SelectedMuscleGroupId
    {
        get => Content.MuscleGroupId.ToString();
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Empty value");
                Content.MuscleGroupId = Guid.Empty;
            }
            else
            {
                Console.WriteLine("Not empty value");
                Content.MuscleGroupId = Guid.Parse(value);
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        var muscleGroupId = Content.MuscleGroupId;
        this.MuscleGroups.Add(new MuscleGroupDto(Guid.Empty, "Select muscle group", ""));
        var muscleGroups = await MuscleGroupHttpService.GetAllAsync();
        foreach (var muscleGroup in muscleGroups)
        {
            this.MuscleGroups.Add(muscleGroup);
        }

        if (muscleGroupId != Guid.Empty)
        {
            SelectedMuscleGroupId = muscleGroupId.ToString();
        }
        await base.OnInitializedAsync();
    }

    private async Task SaveAsync()
    {
        await Dialog.CloseAsync(Content);
    }

    private async Task CancelAsync()
    {
        await Dialog.CancelAsync();
    }
}
