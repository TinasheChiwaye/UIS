using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Funeral.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Vendor scripts
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-2.1.1.min.js"));


            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-1.10.2.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-2.1.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include("~/Scripts/jquery-ui-1.8.20.min.js"));

            // jQuery Validation
            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include("~/Scripts/app/inspinia.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include("~/Scripts/plugins/slimScroll/jquery.slimscroll.min.js"));

            // jQuery plugins
            bundles.Add(new ScriptBundle("~/plugins/metsiMenu").Include("~/Scripts/plugins/metisMenu/jquery.metisMenu.js"));

            bundles.Add(new ScriptBundle("~/plugins/pace").Include("~/Scripts/plugins/pace/pace.min.js"));

            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.min.css", "~/Content/style.css"));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include("~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            //bundles.Add(new ScriptBundle("~/plugins/datatables").Include("~/Scripts/plugins/dataTables/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/plugins/jquerydatatables").Include("~/Scripts/plugins/dataTables/jquery.dataTables.js"));
            bundles.Add(new StyleBundle("~/Content/datatablecss").Include("~/Content/DataTables/css/jquery.dataTables.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js"
                ));


            #region For MVC
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //           "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/jquery-ui").Include("~/Content/jquery-ui.min.css"));
            #endregion

        }

    }
  
}