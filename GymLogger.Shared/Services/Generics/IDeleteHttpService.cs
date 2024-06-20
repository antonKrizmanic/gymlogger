namespace GymLogger.Shared.Services.Generics;

public interface IDeleteHttpService
{
    Task DeleteAsync(Guid id);
}
