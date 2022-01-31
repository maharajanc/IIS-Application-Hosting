﻿using mpex.deployment.web.App_Start;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace mpex.deployment.web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
