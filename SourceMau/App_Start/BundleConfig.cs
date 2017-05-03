using System.Web;
using System.Web.Optimization;

namespace SourceMau
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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/chainstyle").Include(
                "~/Content/backend/chainstyle/css/Style.css",
                "~/Content/backend/chainstyle/css/bootstrap.min.css",
                "~/Content/backend/chainstyle/css/bootstrap-override.css",
                "~/Content/backend/chainstyle/css/style.default.css",
                "~/Content/backend/chainstyle/css/weather-icons.min.css",
                "~/Content/backend/chainstyle/css/jquery-ui-1.10.3.css",
                "~/Content/backend/chainstyle/css/font-awesome.min.css",
                "~/Content/backend/chainstyle/css/animate.min.css",
                "~/Content/backend/chainstyle/css/animate.delay.css",
                "~/Content/backend/chainstyle/css/toggles.css",
                "~/Content/backend/chainstyle/css/pace.css"));
        }
    }
}
