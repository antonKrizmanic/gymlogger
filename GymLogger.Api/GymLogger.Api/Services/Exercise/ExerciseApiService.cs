using AutoMapper;
using GymLogger.Core.CodeExtensions;
using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.Paging.Interfaces;
using GymLogger.Exceptions;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.Paging;
using GymLogger.Shared.Services;

namespace GymLogger.Api.Services.Exercise;

public class ExerciseApiService(IExerciseService service, IMapper mapper) : IExerciseApiService
{
    public async Task<ExerciseDto> GetAsync(Guid id)
    {
        var entity = await service.GetByIdAsync(id);

        if (entity == null)
        {
            throw new GymLoggerEntityNotFoundException($"No Exercise was found for id {id}");
        }

        return entity.MapTo<ExerciseDto>(mapper);
    }

    public async Task<PagedResponseDto<ExerciseDto>> GetPagedListAsync(PagedRequestDto pagedRequestDto)
    {
        var pagedRequest = pagedRequestDto.MapTo<IPagedRequest>(mapper);
        var pagedMuscleGroups = service.GetPagedAsync(pagedRequest);

        var totalItems = await pagedMuscleGroups.TotalCountAsync();
        var pagedItems = await pagedMuscleGroups.GetPageAsync(pagedRequestDto.Page, pagedRequestDto.PageSize);

        return new PagedResponseDto<ExerciseDto>
        {
            PagingData = new PagingDataResponseDto
            {
                Page = pagedRequestDto.Page,
                PageSize = pagedRequestDto.PageSize,
                SortColumn = pagedRequestDto.SortColumn,
                SortDirection = pagedRequestDto.SortDirection,
                TotalItems = totalItems
            },
            Items = pagedItems.MapTo<ICollection<ExerciseDto>>(mapper),
        };
    }

    public async Task<ExerciseDto> CreateAsync(ExerciseCreateDto exerciseCreateDto)
    {
        var exerciseCreate = exerciseCreateDto.MapTo<IExerciseCreate>(mapper);
        var entity = await service.CreateAsync(exerciseCreate);
        return entity.MapTo<ExerciseDto>(mapper);
    }

    public async Task UpdateAsync(ExerciseUpdateDto exerciseUpdateDto)
    {
        var exerciseUpdate = exerciseUpdateDto.MapTo<IExerciseUpdate>(mapper);
        await service.UpdateAsync(exerciseUpdate);
    }

    public Task DeleteAsync(Guid id) =>
        service.DeleteAsync(id);
}
