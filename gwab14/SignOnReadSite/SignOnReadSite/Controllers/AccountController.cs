using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IdentityModel.Services;
using System.IdentityModel.Services.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SignOnReadSite.Models;

namespace SignOnReadSite.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult SignOut()
        {
            WsFederationConfiguration config = FederatedAuthentication.FederationConfiguration.WsFederationConfiguration;

            // Redirect to SignOutCallback after signing out.
            string callbackUrl = Url.Action("SignOutCallback", "Account", routeValues: null, protocol: Request.Url.Scheme);
            SignOutRequestMessage signoutMessage = new SignOutRequestMessage(new Uri(config.Issuer), callbackUrl);
            signoutMessage.SetParameter("wtrealm", IdentityConfig.Realm ?? config.Realm);
            FederatedAuthentication.SessionAuthenticationModule.SignOut();

            return new RedirectResult(signoutMessage.WriteQueryString());
        }

        public ActionResult SignOutCallback()
        {
            if (Request.IsAuthenticated)
            {
                // Redirect to home page if the user is authenticated.
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult SignIn()
        {
            WsFederationConfiguration config = FederatedAuthentication.FederationConfiguration.WsFederationConfiguration;
            string callbackUrl = Url.Action("Index", "Home", routeValues: null, protocol: Request.Url.Scheme);

            SignInRequestMessage signinMessage = new SignInRequestMessage(new Uri(config.Issuer), callbackUrl);
            signinMessage.SetParameter("wtrealm", IdentityConfig.Realm ?? config.Realm);

            return new RedirectResult(signinMessage.WriteQueryString());
        }
    }
}
