using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace SignOnReadSite
{
    public class GraphOperations
    {
        private const string TenantIdClaimType = "http://schemas.microsoft.com/identity/claims/tenantid";
        private const string LoginUrl = "https://login.windows.net/{0}";
        private const string GraphUrl = "https://graph.windows.net";
        private static readonly string ClientId = ConfigurationManager.AppSettings["ida:ClientID"];
        private static readonly string ClientSecret = ConfigurationManager.AppSettings["ida:Password"];

        public static string GetTenantIdFromClaimsPrincipal(ClaimsPrincipal p)
        {
            return p.FindFirst(TenantIdClaimType).Value;
        }

        private GraphConnection _graphConnection;
        public void Initialize(ClaimsPrincipal p)
        {
            var authContext = new AuthenticationContext(String.Format(CultureInfo.InvariantCulture, 
                LoginUrl,
                GetTenantIdFromClaimsPrincipal(p)));

            var credential = new ClientCredential(ClientId, ClientSecret);

            AuthenticationResult authenticationResult = authContext.AcquireToken(GraphUrl, credential);
            string token = authenticationResult.AccessToken;

            var callContext = new CallContext(token, Guid.NewGuid(), "1.21-preview");
            _graphConnection = new GraphConnection(callContext);
        }

        public void Initialize()
        {
            Initialize(ClaimsPrincipal.Current);
        }

        public User GetUser(string userPrincipalName, bool expandGroups)
        {
            var filter = new FilterGenerator();
            filter.FilterExpressions.Add(
             new EqualsFilter(
                 "userPrincipalName",
                 userPrincipalName));

            if (expandGroups)
                filter.ExpandProperties.Add("memberOf");

            PagedResults pagedResults = _graphConnection.List<User>(null, filter);

            if (pagedResults.Results.Count > 0)
            {
                var user = pagedResults.Results[0] as User;
                return user;
            }
            else
            {
                Console.WriteLine("User not found");
                return null;
            }
        }

        public User GetUser(string objectId)
        {
            return _graphConnection.Get<User>(objectId);
        }
    }
}