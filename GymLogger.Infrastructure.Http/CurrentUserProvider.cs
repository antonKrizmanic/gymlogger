using GymLogger.Application.User.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GymLogger.Infrastructure.Http;
internal class CurrentUserProvider(IHttpContextAccessor httpContextAccessor) : ICurrentUserProvider
{
    public string? GetCurrentUserId() => httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}
