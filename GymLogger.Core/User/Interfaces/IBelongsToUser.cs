using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Core.User.Interfaces;
public interface IBelongsToUser
{
    string BelongsToUserId { get; set; }
}
