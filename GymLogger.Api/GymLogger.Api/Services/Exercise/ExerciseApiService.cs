﻿using AutoMapper;
using GymLogger.Core.CodeExtensions;
using GymLogger.Core.Exercise.Interfaces;
using GymLogger.Core.MuscleGroups.Interfaces;
using GymLogger.Core.Paging.Interfaces;
using GymLogger.Exceptions;
using GymLogger.Shared.Models.Exercise;
using GymLogger.Shared.Models.MuscleGroups;
using GymLogger.Shared.Models.Paging;

namespace GymLogger.Api.Services.Exercise;

public class ExerciseApiService(IExerciseService service, IMapper mapper) : IExerciseApiService
{
    public async Task<ExerciseDto> GetById(Guid id)
    {
        var entity = await service.GetByIdAsync(id);

        if (entity == null)
        {
            throw new GymLoggerEntityNotFoundException($"No Exercise was found for id {id}");
        }

        return entity.MapTo<ExerciseDto>(mapper);
    }

    public async Task<PagedResponseDto<ExerciseDto>> GetPagedAsync(PagedRequestDto pagedRequestDto)
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
            Items = pagedItems.MapTo<IEnumerable<ExerciseDto>>(mapper),
        };
    }
}
