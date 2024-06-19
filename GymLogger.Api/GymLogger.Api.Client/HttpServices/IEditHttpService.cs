namespace GymLogger.Api.Client.HttpServices;

public interface IEditHttpService<TDto>
{
    Task<TDto> GetForEditAsync(Guid id);
    Task<HttpResponseMessage> UpdateAsync(TDto dto);
}
