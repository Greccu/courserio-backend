using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Core.Helpers
{
    public static class UserHelper
    {
        public static string GetUsername(this System.Security.Claims.ClaimsPrincipal user)
        {
            return user.FindFirst(x => x.Type == "preferred_username")!.Value;
        }
    }
}
