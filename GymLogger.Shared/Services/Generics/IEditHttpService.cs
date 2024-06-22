namespace GymLogger.Shared.Services.Generics;

public interface IEditHttpService<TDto>
{
    Task UpdateAsync(TDto dto);
}
