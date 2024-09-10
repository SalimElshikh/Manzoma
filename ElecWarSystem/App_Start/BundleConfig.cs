using System.Web.Optimization;

namespace ElecWarSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
           "~/Content/bootstrap.css",
           "~/Content/bootstrap-grid.css",
           "~/Content/bootstrap-theme.css",
           "~/Content/LoginStyle.css",
           "~/Content/EmailListStyle.css",
           "~/Content/New_Design/MainBodyStyle.css",
           "~/Content/New_Design/fonts.min.css"
));

        }
    }
}
