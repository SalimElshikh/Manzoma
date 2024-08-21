﻿using System.Web.Mvc;
using System.Web.Routing;

namespace ElecWarSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "GetData",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "KharegTmarkozs", action = "GetData", id = UrlParameter.Optional }
            );
        }
    }
}
