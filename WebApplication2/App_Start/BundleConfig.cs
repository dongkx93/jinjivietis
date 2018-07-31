using System.Web;
using System.Web.Optimization;

namespace WebApplication2
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jsdatepicker").Include(
                "~/Scripts/bootstrap-datepicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/form").Include(
                "~/Scripts/form.js"));
      

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 開発と学習には、Modernizr の開発バージョンを使用します。次に、実稼働の準備が
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css" ,
                      "~/Content/bootstrap-datepicker.min.css"));

            bundles.Add(new StyleBundle("~/Content/loginCss").Include(
                "~/Content/bootstrap.css",
                "~/Content/form-elements.css",
                "~/Content/style.css"));
        }
    }
}
