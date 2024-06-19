using AutoMapper;
using GymLogger.Core.Exercise;
using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Core.Paging;
using GymLogger.Core.Paging.Interfaces;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Models.Paging;

namespace GymLogger.Api;

public class ApiMapperProfile : Profile
{
    public ApiMapperProfile()
    {
        MapPagingModels();
        MapMuscleGroupModels();
        MapExerciseModels();
    }

    private void MapPagingModels()
    {
        this.CreateMap<PagedRequestDto, PagedRequest>();
        this.CreateMap<PagedRequestDto, IPagedRequest>().As<PagedRequest>();
    }

    private void MapMuscleGroupModels()
    {
        this.CreateMap<IMuscleGroup, MuscleGroupDto>();

        // Create and Edit DTO mapping`
    }

    private void MapExerciseModels()
    {
        this.CreateMap<IExercise, ExerciseDto>();

        this.CreateMap<ExerciseCreateDto, ExerciseCreate>();
        this.CreateMap<ExerciseCreateDto, IExerciseCreate>().As<ExerciseCreate>();

        this.CreateMap<ExerciseUpdateDto, ExerciseUpdate>();
        this.CreateMap<ExerciseUpdateDto, IExerciseUpdate>().As<ExerciseUpdate>();
    }
}
