using System.Web.Http;
using System.Web.Mvc;

namespace BetterSite.UI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });///

           // AdminConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}