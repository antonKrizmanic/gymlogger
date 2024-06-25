using AutoMapper;
using GymLogger.Core.CodeExtensions;
using GymLogger.Core.Workout.Interfaces;
using GymLogger.Exceptions;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Models.Workout;
using GymLogger.Shared.Services;

namespace GymLogger.Api.Services.Workout;

internal class WorkoutApiService(IWorkoutService service, IMapper mapper) : IWorkoutApiService
{
    public async Task<PagedResponseDto<WorkoutDto>> GetPagedListAsync(WorkoutPagedRequestDto request)
    {
        var pagedRequest = request.MapTo<IWorkoutPagedRequest>(mapper);
        var pagedWorkouts = service.GetPagedAsync(pagedRequest);

        var totalItems = await pagedWorkouts.TotalCountAsync();
        var pagedItems = await pagedWorkouts.GetPageAsync(request.Page, request.PageSize);

        return new PagedResponseDto<WorkoutDto>
        {
            PagingData = new PagingDataResponseDto
            {
                Page = request.Page,
                PageSize = request.PageSize,
                SortColumn = request.SortColumn,
                SortDirection = request.SortDirection,
                TotalItems = totalItems
            },
            Items = pagedItems.MapTo<ICollection<WorkoutDto>>(mapper),
        };
    }

    public async Task<WorkoutDto> GetAsync(Guid id)
    {
        var entity = await service.GetByIdAsync(id) ?? throw new GymLoggerEntityNotFoundException($"No Workout was found for id {id}");

        return entity.MapTo<WorkoutDto>(mapper);
    }

    public async Task<WorkoutDto> CreateAsync(WorkoutCreateDto dto)
    {
        var workoutCreate = dto.MapTo<IWorkoutCreate>(mapper);
        var workout = await service.CreateAsync(workoutCreate);
        return workout.MapTo<WorkoutDto>(mapper);
    }

    public async Task UpdateAsync(WorkoutUpdateDto dto)
    {
        var workout = dto.MapTo<IWorkoutUpdate>(mapper);
        await service.UpdateAsync(workout);
    }

    public Task DeleteAsync(Guid id) =>
        service.DeleteAsync(id);
}
