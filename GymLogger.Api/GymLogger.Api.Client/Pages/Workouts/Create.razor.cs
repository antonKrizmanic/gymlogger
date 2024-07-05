using GymLogger.Api.Client.Components;
using GymLogger.Api.Client.Models.Workout;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.ExerciseWorkout;
using GymLogger.Shared.Models.Workout;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Workouts;

public partial class Create : BaseComponent
{
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IExerciseApiService ExerciseApiService { get; set; } = default!;
    [Inject] private IWorkoutApiService WorkoutApiService { get; set; } = default!;

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
            var results = await this.ExerciseApiService.GetPagedListAsync(new ExercisePagedRequestDto() { Page = 0, PageSize = int.MaxValue, SortColumn = "Name", SortDirection = Common.Enums.SortDirection.Ascending });
            this.Exercises = results.Items;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    private async Task SaveAsync()
    {
        this.Model.Exercises = AddedExercises.Select(x => new ExerciseWorkoutCreateDto()
        {
            ExerciseId = x.Exercise.Id.ToString(),
            Note = x.Note,
            Sets = x.Sets,
            Index = x.Index
        }).ToList();

        try
        {
            await this.WorkoutApiService.CreateAsync(this.Model);
            this.ToastService.ShowSuccess("Trening uspješno dodan.");
            this.NavigationManager.NavigateTo("/workouts");
        }
        catch (Exception)
        {
            this.ToastService.ShowError("Dodavanje treninga nije uspjelo.");
        }
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
        // Combobox is using name, don't know how to change that at the moment
        var addedExercise = this.Exercises.FirstOrDefault(x => x.Name == _exerciseWorkoutModel.ExerciseId);
        if (addedExercise != null)
        {
            var index = this.AddedExercises.Count + 1;
            this.AddedExercises.Add(new() { Exercise = addedExercise, Note = _exerciseWorkoutModel.Note, Index = index});
        }
        this._exerciseWorkoutModel = new ExerciseWorkoutCreateDto();
    }
}