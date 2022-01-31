using System.Web.Optimization;

namespace mpex.deployment.web.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                       "~/Scripts/dashboard.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                    "~/Scripts/dashboard.js"));

            bundles.Add(new ScriptBundle("~/bundles/Popper").Include(
                      "~/Scripts/popper.js"));

            bundles.Add(new ScriptBundle("~/bundles/feather").Include(
                      "~/Scripts/feather-icons.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/dashboard.css"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                     "~/Content/dashboard.css"));

            bundles.Add(new StyleBundle("~/Content/Calender").Include(
                     "~/Content/calender/daygrid.main.min.css",
                     "~/Content/calender/main.css"));

            bundles.Add(new ScriptBundle("~/bundles/Calender").Include(
                   "~/Scripts/calender/daygrid.main.min.js",
                    "~/Scripts/calender/main.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}