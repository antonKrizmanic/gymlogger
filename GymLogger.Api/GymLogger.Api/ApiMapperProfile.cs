﻿using AutoMapper;
using GymLogger.Core.Exercise;
using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Core.Paging;
using GymLogger.Core.Paging.Interfaces;
using GymLogger.Core.Workout;
using GymLogger.Core.Workout.Interfaces;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Models.Workout;

namespace GymLogger.Api;

public class ApiMapperProfile : Profile
{
    public ApiMapperProfile()
    {
        MapPagingModels();
        MapMuscleGroupModels();
        MapExerciseModels();
        MapWorkoutModels();
    }

    private void MapPagingModels()
    {
        this.CreateMap<PagedRequestDto, PagedRequest>();
        this.CreateMap<PagedRequestDto, IPagedRequest>().As<PagedRequest>();
    }

    private void MapMuscleGroupModels()
    {
        this.CreateMap<IMuscleGroup, MuscleGroupDto>();
    }

    private void MapExerciseModels()
    {
        this.CreateMap<IExercise, ExerciseDto>()
            .ForCtorParam("IsPublic", opt => opt.MapFrom(src => src.BelongsToUserId == null));

        this.CreateMap<ExerciseCreateDto, ExerciseCreate>();
        this.CreateMap<ExerciseCreateDto, IExerciseCreate>().As<ExerciseCreate>();

        this.CreateMap<ExerciseUpdateDto, ExerciseUpdate>();
        this.CreateMap<ExerciseUpdateDto, IExerciseUpdate>().As<ExerciseUpdate>();

        this.CreateMap<ExercisePagedRequestDto, ExercisePagedRequest>();
        this.CreateMap<ExercisePagedRequestDto, IExercisePagedRequest>().As<ExercisePagedRequest>();
    }

    private void MapWorkoutModels()
    {
        this.CreateMap<IWorkout, WorkoutDto>();

        this.CreateMap<WorkoutCreateDto, WorkoutCreate>();
        this.CreateMap<WorkoutCreateDto, IWorkoutCreate>().As<WorkoutCreate>();

        this.CreateMap<WorkoutUpdateDto, WorkoutUpdate>();
        this.CreateMap<WorkoutUpdateDto, IWorkoutUpdate>().As<WorkoutUpdate>();

        this.CreateMap<WorkoutPagedRequestDto, WorkoutPagedRequest>();
        this.CreateMap<WorkoutPagedRequestDto, IWorkoutPagedRequest>().As<WorkoutPagedRequest>();
    }
}
