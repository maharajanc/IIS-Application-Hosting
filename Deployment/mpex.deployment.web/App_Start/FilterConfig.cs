using mpex.deployment.web.Filters;
using System.Web.Mvc;

namespace mpex.deployment.web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           // filters.Add(new HandleErrorAttribute());
            filters.Add(new ServerSelectFilter());
           filters.Add(new AuthorizeAttribute());
        }
    }
}