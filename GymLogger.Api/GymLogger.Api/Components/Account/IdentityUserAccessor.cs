using GymLogger.Infrastructure.Database.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace GymLogger.Api.Components.Account;
internal sealed class IdentityUserAccessor(UserManager<DbApplicationUser> userManager, IdentityRedirectManager redirectManager)
{
    public async Task<DbApplicationUser> GetRequiredUserAsync(HttpContext context)
    {
        var user = await userManager.GetUserAsync(context.User);

        if (user is null)
        {
            redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
        }

        return user;
    }
}
