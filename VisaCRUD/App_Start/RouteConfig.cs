using System.Web.Mvc;
using System.Web.Routing;

namespace VisaCRUD
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "{countryId}",
                defaults: new { controller = "Visa", action = "Info" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Visa", action = "Info", id = UrlParameter.Optional }
            );
        }
    }
}
