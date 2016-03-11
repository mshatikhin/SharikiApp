using System.Web.Optimization;

namespace SharikiApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var scriptBundle = new ScriptBundle("~/bundle/jsfiles");
            scriptBundle.Include("~/Scripts/jquery-{version}.js");
            scriptBundle.Include("~/Scripts/bootstrap.js");
            scriptBundle.Include("~/Scripts/knockout-{version}.js");
            scriptBundle.Include("~/Scripts/knockout.mapping-latest.js");            
            scriptBundle.Include("~/Scripts/respond.js");
            scriptBundle.Include("~/Scripts/moment.js");
            scriptBundle.Include("~/Scripts/moment-with-locales.js");
            scriptBundle.Include("~/Scripts/spin.js");            
            scriptBundle.Include("~/Scripts/share.js");
            scriptBundle.Include("~/Scripts/jquery.growl.js");            
            scriptBundle.Include("~/Scripts/client/*.js");

            bundles.Add(scriptBundle);

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/bundle/Content/css")
                .Include("~/Content/jquery.growl.css")
                .Include("~/Content/bootstrap-datetimepicker-build.min.css")
                .Include("~/Content/global.css")
                );
        }
    }
}
