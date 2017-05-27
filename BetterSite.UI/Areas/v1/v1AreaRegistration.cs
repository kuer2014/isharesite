using System.Web.Http;
using System.Web.Mvc;

namespace BetterSite.UI.Areas.v1
{
    public class v1AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "v1";
            }
        }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
        name: "v1_SiteDetail",
        url: "v1/Sites/{SiteCode}",
        defaults: new { controller = "Sites", action = "Detail", SiteCode = UrlParameter.Optional },
         namespaces: new string[] { "BetterSite.UI.Areas.v1.Controllers" },
           constraints: new
           {
               SiteCode = @"SITE\d{13}"
           }
    );
            context.MapRoute(
              name: "v1_Site",
              url: "v1/Sites/{TypeCode}",
              defaults: new { controller = "Sites", action = "Index", TypeCode = UrlParameter.Optional },
               namespaces: new string[] { "BetterSite.UI.Areas.v1.Controllers" },
             constraints: new
             {
                 TypeCode = @"[a-z]{2}"
             }
          );
            context.MapRoute(
                "v1_Default",
                "v1/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });///

           // AdminConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}