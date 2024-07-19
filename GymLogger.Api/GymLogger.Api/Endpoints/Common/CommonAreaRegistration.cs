using GymLogger.Shared.Constants;

namespace GymLogger.Api.Endpoints.Common;

public static class CommonAreaRegistration
{
    public static WebApplication UseApplicationManagerApi(this WebApplication app)
    {
        return app
            .MapApplicationManagerApiEndpoints(ApiRoutes.ApplicationManagement, "ApplicationManagement")
            .MapMuscleGroupApiEndpoints(ApiRoutes.MuscleGroup, "MuscleGroup")
            .MapExerciseApiEndpoints(ApiRoutes.Exercise, "Exercise")
            .MapWorkoutApiEndpoints(ApiRoutes.Workout, "Workout")
            .MapExerciseWorkoutApiEndpoints(ApiRoutes.ExerciseWorkout, "ExerciseWorkout");
    }
}
