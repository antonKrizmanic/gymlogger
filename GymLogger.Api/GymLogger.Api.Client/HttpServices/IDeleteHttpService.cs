namespace GymLogger.Api.Client.HttpServices;

public interface IDeleteHttpService
{
    Task DeleteAsync(Guid id);
}
