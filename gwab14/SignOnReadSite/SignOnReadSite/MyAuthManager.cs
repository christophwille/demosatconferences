using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.Azure.ActiveDirectory.GraphClient;

namespace SignOnReadSite
{
    public class MyAuthManager : ClaimsAuthenticationManager
    {
        public const string ClaimsIssuerName = "MyApp";

        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            if (incomingPrincipal != null && incomingPrincipal.Identity.IsAuthenticated)
            {
                string upn = incomingPrincipal.Identity.Name;

                try
                {
                    var graphOps = new GraphOperations();
                    graphOps.Initialize(incomingPrincipal);

                    var user = graphOps.GetUser(upn, expandGroups: true);
                    List<Group> groups = user.MemberOf.OfType<Group>().Select(mo => (Group)mo).ToList();

                    var roleClaims = groups.Select(g => new Claim(ClaimTypes.Role, g.DisplayName, null, ClaimsIssuerName));
                    ((ClaimsIdentity)incomingPrincipal.Identity).AddClaims(roleClaims);

                    return incomingPrincipal;
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                }
            }

            return null;
        }
    }
}