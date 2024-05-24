using Microsoft.AspNetCore.Identity;

namespace GymLogger.Infrastructure.Database.Models.Identity;
public class DbApplicationUserRole : IdentityUserRole<string>
{
    
    public virtual DbApplicationUser User { get; set; }
    public virtual DbApplicationRole Role { get; set; }

    public DbApplicationUserRole()
    {

    }

    public DbApplicationUserRole(string userId, string roleId)
    {
        this.RoleId = roleId;
        this.UserId = userId;
    }

    public DbApplicationUserRole(DbApplicationUser user, DbApplicationRole role)
    {
        this.User = user;
        this.Role = role;
    }
}
