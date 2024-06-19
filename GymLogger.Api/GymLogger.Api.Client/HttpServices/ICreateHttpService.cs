namespace GymLogger.Api.Client.HttpServices;

public interface ICreateHttpService<in TDto>
{
    Task<HttpResponseMessage> CreateAsync(TDto dto);
}
