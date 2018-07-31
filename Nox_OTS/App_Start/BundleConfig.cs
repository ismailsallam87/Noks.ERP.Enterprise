using System.Web;
using System.Web.Optimization;

namespace Nox_OTS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/assets/plugins/jquery-1.10.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery/migrate").Include(
                        "~/assets/plugins/jquery-migrate-1.2.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/assets/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.js",
                        "~/assets/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/assets/plugins/jquery.blockui.min.js",
                        "~/assets/plugins/jquery.cokie.min.js",
                        "~/assets/plugins/uniform/jquery.uniform.min.js"
                        ));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/assets/plugins/bootstrap/js/bootstrap.min.js",
                     "~/assets/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/assets/plugins/bootstrap/css/bootstrap.min.css",
                      "~/assets/plugins/uniform/css/uniform.default.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/Theme/css").Include(
                      "~/assets/css/style-metronic.css",
                      "~/assets/css/style.css",
                      "~/assets/css/style-responsive.css",
                      "~/assets/css/plugins.css",
                      "~/assets/css/themes/default.css",
                      "~/assets/css/custom.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/Plugins/gritter").Include(
                   "~/assets/css/print.css"
                   ));

            bundles.Add(new StyleBundle("~/Content/Plugins/gritter").Include(
                    "~/assets/plugins/gritter/css/jquery.gritter.css"
                    ));
            bundles.Add(new StyleBundle("~/Content/Plugins/daterangepicker").Include(
                    "~/assets/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css"
                    ));
            bundles.Add(new StyleBundle("~/Content/Plugins/fullcalendar").Include(
                    "~/assets/plugins/fullcalendar/fullcalendar/fullcalendar.css"
                    ));
            bundles.Add(new StyleBundle("~/Content/Plugins/jqvmap").Include(
                    "~/assets/plugins/jqvmap/jqvmap/jqvmap.css"
                    ));
            bundles.Add(new StyleBundle("~/Content/Plugins/piechart").Include(
                    "~/assets/plugins/jquery-easy-pie-chart/jquery.easy-pie-chart.css"
                    ));
        }
    }
}
