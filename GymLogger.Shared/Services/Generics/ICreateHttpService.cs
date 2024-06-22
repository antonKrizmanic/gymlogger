namespace GymLogger.Shared.Services.Generics;

public interface ICreateHttpService<in TCreateDto, TDto>
{
    Task<TDto> CreateAsync(TCreateDto dto);
}
