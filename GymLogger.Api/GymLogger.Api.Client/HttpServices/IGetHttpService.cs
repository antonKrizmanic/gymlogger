namespace GymLogger.Api.Client.HttpServices;

public interface IGetHttpService<TDto>
{
    Task<TDto> GetAsync(Guid id);
}
