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
            /// /Sites/SITE1494393044614
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
            /// /ZY/2.html           
            routes.MapRoute(
              name: "SiteListByType",
              url: "{TypeCode}/{Page}.html",
              defaults: new { controller = "Sites", action = "Index",  Page = 1 },
               namespaces: new string[] { "BetterSite.UI.Controllers" },
             constraints: new
             {
                 TypeCode = @"[a-z]{2,10}",
                 Page = @"\d{0,5}"
             }
          );
            ///
            /// /zy/tag/学习/1.html
            routes.MapRoute(
              name: "SiteListByTag",
              url: "{TypeCode}/{Tag}/{Page}.html",
              defaults: new { controller = "Sites", action = "Index", Page =1 },
               namespaces: new string[] { "BetterSite.UI.Controllers" },
             constraints: new
             {
                 TypeCode = @"[a-z]{2,10}",
                 Page= @"\d{0,5}"
             }
          );
            /// /2.html           
            routes.MapRoute(
              name: "AllSiteList",
              url: "{Page}.html",
              defaults: new { controller = "Sites", action = "Index", Page = 1 },
               namespaces: new string[] { "BetterSite.UI.Controllers" },
             constraints: new
             {              
                 Page = @"\d{0,5}"
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