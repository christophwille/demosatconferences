using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace WaadWebApiDemoVs2013.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            var p = ClaimsPrincipal.Current;
            return new string[] { p.Identities.First().Name };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
