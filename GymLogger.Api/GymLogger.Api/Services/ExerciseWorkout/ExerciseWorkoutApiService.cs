using AutoMapper;
using GymLogger.Core.CodeExtensions;
using GymLogger.Core.ExerciseWorkout.Interfaces;
using GymLogger.Shared.Models.ExerciseWorkout;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Services;

namespace GymLogger.Api.Services.ExerciseWorkout;

internal class ExerciseWorkoutApiService(IExerciseWorkoutService service, IMapper mapper) : IExerciseWorkoutApiService
{
    public async Task<ExerciseWorkoutDto?> GetLatestForCurrentUserAsync(Guid exerciseId, Guid? workoutId)
    {
        var entity = await service.GetLatestForCurrentUserAsync(exerciseId, workoutId);

        if (entity == null)
        {
            return null;
        }

        return entity.MapTo<ExerciseWorkoutDto>(mapper);
    }

    public async Task<PagedResponseDto<ExerciseWorkoutDetailDto>> GetPagedListAsync(ExerciseWorkoutPagedRequestDto pagedRequestDto)
    {
        var pagedRequest = pagedRequestDto.MapTo<IExerciseWorkoutPagedRequest>(mapper);
        var pagedMuscleGroups = service.GetPagedAsync(pagedRequest);

        var totalItems = await pagedMuscleGroups.TotalCountAsync();
        var pagedItems = await pagedMuscleGroups.GetPageAsync(pagedRequestDto.Page, pagedRequestDto.PageSize);

        return new PagedResponseDto<ExerciseWorkoutDetailDto>
        {
            PagingData = new PagingDataResponseDto
            {
                Page = pagedRequestDto.Page,
                PageSize = pagedRequestDto.PageSize,
                SortColumn = pagedRequestDto.SortColumn,
                SortDirection = pagedRequestDto.SortDirection,
                TotalItems = totalItems
            },
            Items = pagedItems.MapTo<ICollection<ExerciseWorkoutDetailDto>>(mapper),
        };
    }
}
