using System.Web.Mvc;
using System.Web.Routing;


namespace Funeral.Web.App_Start
{
    public static class RouteConfig
    {
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    FriendlyUrlSettings settings = new FriendlyUrlSettings();
        //    settings.AutoRedirectMode = RedirectMode.Permanent;
        //    routes.EnableFriendlyUrls(settings);
        //}
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AdminArea",
                url: "Admin/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
