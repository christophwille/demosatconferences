using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Services.Client;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.WindowsAzure.ActiveDirectory;
using Microsoft.WindowsAzure.ActiveDirectory.GraphHelper;

namespace WaadSampleVs2013
{
    public class WaadRolesClaimsAuthenticationManager : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            if (incomingPrincipal != null && incomingPrincipal.Identity.IsAuthenticated)
            {
                string upn = incomingPrincipal.Identity.Name;

                var ds = GetAuthenticatedDirectoryDataService(incomingPrincipal);

                // One call only when using Expand
                DataServiceQuery<User> query = ds.users.Expand("memberOf");
                User user = query.Where(it => (it.objectId == upn)).SingleOrDefault();
                
                // There could be "Role" too, that's why we filter with OfType<>
                List<Group> groups = user.memberOf.OfType<Group>().Select(mo => (Group)mo).ToList();    

                var roleClaims = groups.Select(g => new Claim(ClaimTypes.Role, g.displayName, null, "CLAIMISSUER_GRAPH"));
                ((ClaimsIdentity)incomingPrincipal.Identity).AddClaims(roleClaims);
            }

            return incomingPrincipal;
        }

        private DirectoryDataService GetAuthenticatedDirectoryDataService(ClaimsPrincipal incomingPrincipal)
        {
            //get the tenantName
            string tenantName = incomingPrincipal.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;

            // retrieve the clientId and password values from the Web.config file
            string clientId = ConfigurationManager.AppSettings["ida:ClientID"];
            string password = ConfigurationManager.AppSettings["ida:Password"];

            // get a token using the helper
            AADJWTToken token = DirectoryDataServiceAuthorizationHelper.GetAuthorizationToken(tenantName, clientId, password);

            // initialize a graphService instance using the token acquired from previous step
            return new DirectoryDataService(tenantName, token);
        }
    }
}