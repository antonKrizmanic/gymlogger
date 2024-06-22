using AutoMapper;
using GymLogger.Core.Exercise;
using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.MuscleGroups;
using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Infrastructure.Database.Models.Exercise;
using GymLogger.Infrastructure.Database.Models.MuscleGroups;

namespace GymLogger.Infrastructure.Database;
public class InfrastructureDatabaseMapperProfile : Profile
{
    public InfrastructureDatabaseMapperProfile()
    {
        MapMuscleGroupModels();
        MapExerciseModels();
    }

    private void MapMuscleGroupModels()
    {
        this.CreateMap<DbMuscleGroup, MuscleGroup>();
        this.CreateMap<DbMuscleGroup, IMuscleGroup>().As<MuscleGroup>();
    }

    private void MapExerciseModels()
    {
        this.CreateMap<DbExercise, Exercise>();

        this.CreateMap<DbExercise, IExercise>()
            .As<Exercise>();
    }
}
