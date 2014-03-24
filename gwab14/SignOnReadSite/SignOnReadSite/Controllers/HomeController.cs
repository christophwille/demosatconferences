using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignOnReadSite.Models;

namespace SignOnReadSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [Authorize]
        public ActionResult UserProfile()
        {
            string aadObjectIdOfUser = ClaimsPrincipal.Current.GetObjectId();

            var graphOps = new GraphOperations();
            graphOps.Initialize();

            var user = graphOps.GetUser(aadObjectIdOfUser);

            var profile = new UserProfile()
            {
                DisplayName = user.DisplayName,
                GivenName = user.GivenName,
                Surname = user.Surname
            };

            return View(profile);
        }

        [Authorize(Roles = "Demo Group A")]
        public ActionResult RoleAOnly()
        {
            return View();
        }

        [Authorize(Roles = "Demo Group B")]
        public ActionResult RoleBOnly()
        {
            return View();
        }
    }
}