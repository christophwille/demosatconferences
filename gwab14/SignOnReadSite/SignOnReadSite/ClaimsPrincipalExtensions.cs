using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace SignOnReadSite
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetObjectId(this ClaimsPrincipal current)
        {
            var id = current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier");
            return id.Value;
        }

        public static void DebugPrintAllClaims(this ClaimsPrincipal current)
        {
            foreach (Claim claim in current.Claims)
            {
                Debug.WriteLine("CLAIM TYPE: " + claim.Type + "; CLAIM VALUE: " + claim.Value);
            }
        }
    }
}