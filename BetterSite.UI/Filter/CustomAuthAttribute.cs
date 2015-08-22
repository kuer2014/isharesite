using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Filter
{
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        private bool localAllowed;
        public CustomAuthAttribute(bool allowedParam)
        {
            localAllowed = allowedParam;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsLocal)
            {
              // httpContext.Response.Redirect("Home/Index");
                return localAllowed;
            }
            else
            {
                return true;
            }
        }
    }
}