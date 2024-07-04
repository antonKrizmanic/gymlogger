namespace GymLogger.Application.User.Interfaces;
public interface ICurrentUserProvider
{
    string? GetCurrentUserId();
}
