namespace GymLogger.Shared.Services.Generics;

public interface IGetHttpService<TDto>
{
    Task<TDto> GetAsync(Guid id);
}
