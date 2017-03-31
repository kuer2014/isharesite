using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BetterSite.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
          name: "SiteDetail",
          url: "Sites/{SiteCode}",
          defaults: new { controller = "Sites", action = "Detail", SiteCode = UrlParameter.Optional },
           namespaces: new string[] { "BetterSite.UI.Controllers" },
             constraints: new
             {
                 SiteCode = @"SITE\d{13}"
             }
      );
            routes.MapRoute(
              name: "Site",
              url: "Sites/{TypeCode}",
              defaults: new { controller = "Sites", action = "Index", TypeCode = UrlParameter.Optional },
               namespaces: new string[] { "BetterSite.UI.Controllers" },
             constraints: new
             {
                 TypeCode = @"[a-z]{2}"
             }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                //constraints: new string[] { "BetterSite.UI.Controllers" }
                namespaces: new string[] { "BetterSite.UI.Controllers" }

            );
        }
    }
}