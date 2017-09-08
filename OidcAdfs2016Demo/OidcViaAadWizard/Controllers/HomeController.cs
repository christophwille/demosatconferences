using System;
using System.Security.Claims;
using System.Web.Mvc;

namespace OidcViaAadWizard.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // https://docs.microsoft.com/en-us/windows-server/identity/ad-fs/development/customize-id-token-ad-fs-2016
        public ActionResult About()
        {
            // https://jwt.io/ + Fiddler -> Token debugging
            // https://msdn.microsoft.com/en-us/library/microsoft.identitymodel.claims.claimtypes_members.aspx
            ClaimsPrincipal cp = ClaimsPrincipal.Current;

            string userName = cp.FindFirst(ClaimTypes.GivenName).Value;

            // https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/issues/415 
            // --> that's why I am *not* using "Name ID" nameidentifier (and for laziness abusing emailaddress)
            string samAccountName = cp.FindFirst(ClaimTypes.Email).Value;
            string roles = cp.FindFirst(ClaimTypes.Role).Value;

            ViewBag.Message = $"Hello given {userName}, sam {samAccountName}, roles {roles}!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}