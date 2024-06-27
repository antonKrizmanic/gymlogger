using AutoMapper;
using GymLogger.Core.Exercise;
using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.ExerciseWorkout;
using GymLogger.Core.ExerciseWorkout.Interfaces;
using GymLogger.Core.MuscleGroups;
using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Core.Workout;
using GymLogger.Core.Workout.Interfaces;
using GymLogger.Infrastructure.Database.Models.Exercise;
using GymLogger.Infrastructure.Database.Models.ExerciseWorkout;
using GymLogger.Infrastructure.Database.Models.MuscleGroups;
using GymLogger.Infrastructure.Database.Models.Workout;

namespace GymLogger.Infrastructure.Database;
public class InfrastructureDatabaseMapperProfile : Profile
{
    public InfrastructureDatabaseMapperProfile()
    {
        MapMuscleGroupModels();
        MapExerciseModels();
        MapWorkoutModels();
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

    private void MapWorkoutModels()
    {
        this.CreateMap<DbWorkout, Workout>();
        this.CreateMap<DbWorkout, IWorkout>().As<Workout>();

        this.CreateMap<DbExerciseWorkout, ExerciseWorkout>();
        this.CreateMap<DbExerciseWorkout, IExerciseWorkout>().As<ExerciseWorkout>();
    }
}
