using GymLogger.Shared.Models.Workout;
using GymLogger.Shared.Services.Generics;

namespace GymLogger.Shared.Services
{
    public interface IWorkoutApiService :
    ICreateHttpService<WorkoutCreateDto, WorkoutDto>,
    IDeleteHttpService,
    IEditHttpService<WorkoutUpdateDto>,
    IPagedHttpService<WorkoutDto, WorkoutPagedRequestDto>,
    IGetHttpService<WorkoutDetailsDto>
    {
        Task<WorkoutUpdateDto> GetForEditAsync(Guid id);
    }
}