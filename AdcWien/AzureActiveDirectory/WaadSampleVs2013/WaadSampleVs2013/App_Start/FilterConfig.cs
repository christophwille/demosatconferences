using System.Web;
using System.Web.Mvc;

namespace WaadSampleVs2013
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
