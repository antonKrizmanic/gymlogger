using AutoMapper;
using GymLogger.Core.MuscleGroups;
using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Infrastructure.Database.Models.MuscleGroups;

namespace GymLogger.Infrastructure.Database;
public class InfrastructureDatabaseMapperProfile : Profile
{
    public InfrastructureDatabaseMapperProfile()
    {
        MapMuscleGroupModels();
    }

    private void MapMuscleGroupModels()
    {
        this.CreateMap<DbMuscleGroup, MuscleGroup>();
        this.CreateMap<DbMuscleGroup, IMuscleGroup>().As<MuscleGroup>();
    }
}
