﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ATTMS.Dashboard
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //    name: "UnAuthorized",
            //    url: "UnAuthorized",
            //    defaults: new { controller = "Accoun", action = "UnAuthorized" }
            //);
            
                

            //    routes.MapRoute(
            //    name: "ChangeDisplayName",
            //    url: "ChangeDisplayName",
            //    defaults: new { controller = "Manage", action = "ChangeDisplayName" }            );
            //routes.MapRoute(
            //   name: "ChangePassword",
            //   url: "ChangePassword",
            //   defaults: new { controller = "Manage", action = "ChangePassword" });

        }
    }
}
