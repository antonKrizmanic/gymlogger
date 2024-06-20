using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Services.Generics;

namespace GymLogger.Shared.Services;
public interface IExerciseApiService :
    ICreateHttpService<ExerciseCreateDto, ExerciseDto>,
    IDeleteHttpService,
    IEditHttpService<ExerciseUpdateDto>,
    IPagedHttpService<ExerciseDto, PagedRequestDto>,
    IGetHttpService<ExerciseDto>
{

}
