using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Infrastructure.Database.Models.Identity;
public class DbApplicationRole : IdentityRole
{
    public string FriendlyName { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public DbApplicationRole()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="IdentityRole"/>.
    /// </summary>
    /// <remarks>
    /// The Id property is initialized to form a new GUID string value.
    /// </remarks>
    /// <param name="roleName">The role name.</param>
    public DbApplicationRole(string roleName) : base(roleName)
    {
    }

    /// <summary>
    /// Gets or sets the user roles.
    /// </summary>
    /// <value>
    /// The user roles.
    /// </value>
    public ICollection<DbApplicationUserRole> UserRoles { get; set; }
}
