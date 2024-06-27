using GymLogger.Api.Client.Components;
using GymLogger.Api.Client.Pages.Workouts.Components;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.ExerciseSet;
using GymLogger.Shared.Models.ExerciseWorkout;
using GymLogger.Shared.Models.Workout;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Workouts;

public partial class Create : BaseComponent
{
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IExerciseApiService ExerciseApiService { get; set; } = default!;

    public ICollection<ExerciseDto> Exercises = [];
    private ICollection<ExerciseSetCreateFormViewModel> AddedExercises = [];
    public WorkoutCreateDto Model { get; set; } = new WorkoutCreateDto();
    private bool _showAddExerciseForm = false;
    private ExerciseWorkoutCreateDto _exerciseWorkoutModel = new ExerciseWorkoutCreateDto();

    protected async override Task OnInitializedAsync()
    {
        await this.LoadDataAsync();
        await base.OnInitializedAsync();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            var result = await this.ExerciseApiService.GetPagedListAsync(new ExercisePagedRequestDto() { Page = 0, PageSize = int.MaxValue, SortColumn = "Name", SortDirection = Common.Enums.SortDirection.Ascending });
            this.Exercises.Add(new(Guid.Empty, "Select Exercise", Guid.Empty, "", "", Common.Enums.ExerciseLogType.Time, false));
            foreach (var item in result.Items)
            {
                this.Exercises.Add(item);
            }
            Console.WriteLine($"Exercises count: {this.Exercises.Count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    private async Task SaveAsync()
    {

    }

    private void Cancel()
    {
        this.NavigationManager.NavigateTo("/workouts");
    }

    private void OnAddExerciseClick()
    {
        this._showAddExerciseForm = true;
    }

    private void OnAddExerciseFormClose()
    {
        this._showAddExerciseForm = false;
        this._exerciseWorkoutModel = new();
    }

    private void OnAddExerciseFormSave()
    {
        this._showAddExerciseForm = false;
        this.Model.Exercises.Add(_exerciseWorkoutModel);
        var addedExercise = this.Exercises.FirstOrDefault(x => x.Id.ToString() == _exerciseWorkoutModel.ExerciseId);
        if (addedExercise != null)
        {
            this.AddedExercises.Add(new() { Exercise = addedExercise });
        }
        this._exerciseWorkoutModel = new ExerciseWorkoutCreateDto();
    }

    private async Task OnAddSetClick(Guid exerciseId)
    {
        var dto = new ExerciseSetFormViewModel();
        dto.Exercise = this.Exercises.FirstOrDefault(x => x.Id.ToString() == exerciseId.ToString());
        dto.CreateDto = new();
        var dialog = await DialogService.ShowDialogAsync<ExerciseSetFormDialog>(dto, new DialogParameters()
        {
            Title = $"Novi set",
            PreventDismissOnOverlayClick = true,
            PreventScroll = true,
        });

        var result = await dialog.Result;
        if (!result.Cancelled && result.Data != null)
        {
            try
            {
                var addedSet = (ExerciseSetFormViewModel)result.Data;
                //Update this.AddedExercises


                var addedExercise = this.AddedExercises.FirstOrDefault(x => x.Exercise.Id.ToString() == exerciseId.ToString());
                addedExercise.Sets = addedExercise.Sets ?? [];
                addedExercise.Sets.Add(addedSet.CreateDto);

                //await Https.CreateAsync((ExerciseCreateDto)result.Data);
                //await this.LoadDataAsync();
                this.ToastService.ShowSuccess("Set uspješno dodan.");
            }
            catch (Exception)
            {
                this.ToastService.ShowError("Dodavanje set nije uspjelo.");
            }
        }
    }

    private void RemoveExercise(Guid exerciseId)
    {
        Console.WriteLine($"Remove exercise: {exerciseId}");
        var exercise = this.Model.Exercises.FirstOrDefault(x => x.ExerciseId == exerciseId.ToString());
        if (exercise != null)
        {
            this.Model.Exercises.Remove(exercise);
        }
        var addedExercise = this.AddedExercises.FirstOrDefault(x => x.Exercise.Id.ToString() == exerciseId.ToString());
        if (addedExercise != null)
        {
            this.AddedExercises.Remove(addedExercise);
        }
    }
}

public class ExerciseSetCreateFormViewModel
{
    public ICollection<ExerciseSetCreateDto> Sets { get; set; } = [];
    public ExerciseDto Exercise { get; set; }
}
