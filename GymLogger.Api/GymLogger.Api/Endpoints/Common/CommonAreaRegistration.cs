namespace GymLogger.Api.Endpoints.Common;

public static class CommonAreaRegistration
{
    public static WebApplication UseApplicationManagerApi(this WebApplication app)
    {
        return app
            .MapApplicationManagerApiEndpoints("/api/ApplicationManagement", "ApplicationManagement")
            .MapMuscleGroupApiEndpoints("/api/MuscleGroup", "MuscleGroup")
            .MapExerciseApiEndpoints("/api/Exercise", "Exercise");
    }
}
