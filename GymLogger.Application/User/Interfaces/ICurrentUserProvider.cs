using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Application.User.Interfaces;
public interface ICurrentUserProvider
{
    string? GetCurrentUserId();
}
