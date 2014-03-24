using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace SignOnReadSite
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetObjectId(this ClaimsPrincipal current)
        {
            var id = current.FindFirst(ClaimTypes.UserData);
            return id.Value;
        }
    }
}