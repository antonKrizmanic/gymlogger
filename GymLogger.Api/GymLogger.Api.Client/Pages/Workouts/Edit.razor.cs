﻿using GymLogger.Api.Client.Components;
using GymLogger.Api.Client.Models.Workout;
using GymLogger.Api.Client.Pages.Workouts.Components;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.ExerciseSet;
using GymLogger.Shared.Models.ExerciseWorkout;
using GymLogger.Shared.Models.Workout;
using GymLogger.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace GymLogger.Api.Client.Pages.Workouts;

public partial class Edit : BaseComponent
{
    [Parameter] public Guid Id { get; set; }
    
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IExerciseApiService ExerciseApiService { get; set; } = default!;
    [Inject] private IWorkoutApiService WorkoutApiService { get; set; } = default!;

    public ICollection<ExerciseDto> Exercises = [];
    private ICollection<ExerciseSetCreateFormViewModel> AddedExercises = [];
    public WorkoutUpdateDto Model { get; set; } = new WorkoutUpdateDto();
    private bool _showAddExerciseForm = false;
    private ExerciseWorkoutCreateDto _exerciseWorkoutModel = new ExerciseWorkoutCreateDto();

    protected async override Task OnInitializedAsync()
    {
        await this.LoadDataAsync();
        await base.OnInitializedAsync();
    }

    private async Task LoadDataAsync()
    {
        this.Model = await WorkoutApiService.GetForEditAsync(Id);
        try
        {
            var result = await this.ExerciseApiService.GetPagedListAsync(new ExercisePagedRequestDto() { Page = 0, PageSize = int.MaxValue, SortColumn = "Name", SortDirection = Common.Enums.SortDirection.Ascending });
            this.Exercises.Add(new(Guid.Empty, "Select Exercise", Guid.Empty, "", "", Common.Enums.ExerciseLogType.Time, false));
            foreach (var item in result.Items)
            {
                this.Exercises.Add(item);
            }
            
            // Add exercises from model to added exercises
            foreach (var exercise in this.Model.Exercises)
            {
                var addedExercise = this.Exercises.FirstOrDefault(x => x.Id.ToString() == exercise.ExerciseId);
                if (addedExercise != null)
                {
                    this.AddedExercises.Add(new() { Exercise = addedExercise, Note = exercise.Note, Sets = exercise.Sets.ToList() });
                }
            }
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
            Sets = x.Sets
        }).ToList();

        try
        {
            await this.WorkoutApiService.UpdateAsync(this.Model);
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
            this.AddedExercises.Add(new() { Exercise = addedExercise, Note = _exerciseWorkoutModel.Note });
        }
        this._exerciseWorkoutModel = new ExerciseWorkoutCreateDto();
    }

    private async Task OnAddSetClick(Guid exerciseId)
    {
        var dto = new ExerciseSetFormViewModel();
        dto.Exercise = this.Exercises.FirstOrDefault(x => x.Id.ToString() == exerciseId.ToString());
        dto.CreateDto = new() { Id = Guid.NewGuid() };
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

                var addedExercise = this.AddedExercises.FirstOrDefault(x => x.Exercise.Id.ToString() == exerciseId.ToString());
                addedExercise.Sets = addedExercise.Sets ?? [];
                addedSet.CreateDto.Index = addedExercise.Sets.Count + 1;
                addedExercise.Sets.Add(addedSet.CreateDto);

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

    private async Task OnEditSetAsync(EditSetEventArgs args)
    {
        var dto = new ExerciseSetFormViewModel();
        dto.Exercise = this.Exercises.FirstOrDefault(x => x.Id.ToString() == args.ExerciseId.ToString());
        dto.CreateDto = args.Set;
        var dialog = await DialogService.ShowDialogAsync<ExerciseSetFormDialog>(dto, new DialogParameters()
        {
            Title = $"Izmjeni set",
            PreventDismissOnOverlayClick = true,
            PreventScroll = true,
        });

        var result = await dialog.Result;
        if (!result.Cancelled && result.Data != null)
        {
            try
            {
                var edditedSet = (ExerciseSetFormViewModel)result.Data;

                var edditedExercise = this.AddedExercises.FirstOrDefault(x => x.Exercise.Id.ToString() == args.ExerciseId.ToString());
                var oldSet = edditedExercise.Sets.FirstOrDefault(x => x.Id == edditedSet.CreateDto.Id);
                oldSet.Reps = edditedSet.CreateDto.Reps;
                oldSet.Weight = edditedSet.CreateDto.Weight;
                oldSet.Time = edditedSet.CreateDto.Time;

                this.ToastService.ShowSuccess("Set uspješno dodan.");
            }
            catch (Exception)
            {
                this.ToastService.ShowError("Dodavanje set nije uspjelo.");
            }
        }
    }

    private void OnRemoveSet(EditSetEventArgs args)
    {
        var exercise = this.AddedExercises.FirstOrDefault(x => x.Exercise.Id == args.ExerciseId);
        var set = exercise?.Sets.FirstOrDefault(x => x.Id == args.Set.Id);
        if (set != null)
        {
            exercise?.Sets.Remove(set);
        }
    }
}