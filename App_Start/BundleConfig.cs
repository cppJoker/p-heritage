using System.Web;
using System.Web.Optimization;

namespace Projet_Heritage
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                "~/Scripts/bootbox.min.js",
                "~/Scripts/jquery-3.6.0.js",
                "~/Scripts/bootstrap.bundle.min.js",
                "~/DataTables/datatables.min.js",
                "~/Scripts/minimal-autocomplete-bootstrap/src/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site_1.1.css",
                "~/Content/bootstrap.css",
                "~/DataTables/dataTables.css"));
        }
    }
}
