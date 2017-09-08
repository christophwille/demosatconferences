using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

namespace OidcViaAadWizard
{
    public partial class Startup
    {
        // https://docs.microsoft.com/en-us/windows-server/identity/ad-fs/development/enabling-openid-connect-with-ad-fs-2016
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string metadata = ConfigurationManager.AppSettings["ida:Metadata"];
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    MetadataAddress = metadata,
                    RedirectUri = postLogoutRedirectUri, // Azure AD does not enforce the presence of a redirect_uri in the request, but ADFS does. So, we need to add it here
                    PostLogoutRedirectUri = postLogoutRedirectUri,
                    Notifications = new OpenIdConnectAuthenticationNotifications()
                    {
                        // https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-v2-protocols-oidc "server_error" doc
                        // https://samlman.wordpress.com/2015/04/10/how-to-fix-the-openid-access-denied-when-user-wont-grant-rights-at-login/
                        AuthenticationFailed = (context) =>
                        {
                            ////this section added to handle scenario where user logs in, but cancels consenting to rights to read directory profile
                            //string appBaseUrl = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase;
                            //context.ProtocolMessage.RedirectUri = appBaseUrl + "/";

                            ////this is where the magic happens
                            //context.HandleResponse();
                            //context.Response.Redirect(context.ProtocolMessage.RedirectUri);

                            return Task.FromResult(0);

                        }
                    }
                });
        }
    }
}
