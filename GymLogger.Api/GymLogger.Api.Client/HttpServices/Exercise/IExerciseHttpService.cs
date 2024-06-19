using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.Paging;

namespace GymLogger.Api.Client.HttpServices.Exercise;

public interface IExerciseHttpService :
    ICreateHttpService<ExerciseCreateDto>,
    IDeleteHttpService,
    IEditHttpService<ExerciseUpdateDto>,
    IPagedHttpService<ExerciseDto, PagedRequestDto>,
    IGetHttpService<ExerciseDto>
{
}
