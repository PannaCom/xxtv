using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace cms
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //define 1 cai route o day
            routes.MapRoute(
                "newsdetail",
                "news/{title}-{id}",
                new { controller = "News", action = "Details", title = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "projectdetail",
                "project/{title}-{id}",
                new { controller = "Home", action = "ProjectDetail", title = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "projectcat",
                "projects/categories/{title}-{id}",
                new { controller = "Home", action = "ProjectCat", title = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}