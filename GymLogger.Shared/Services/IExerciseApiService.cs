using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Services.Generics;

namespace GymLogger.Shared.Services;
public interface IExerciseApiService :
    ICreateHttpService<ExerciseCreateDto, ExerciseDto>,
    IDeleteHttpService,
    IEditHttpService<ExerciseUpdateDto>,
    IPagedHttpService<ExerciseDto, ExercisePagedRequestDto>,
    IGetHttpService<ExerciseDto>
{

}
