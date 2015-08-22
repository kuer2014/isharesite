using BetterSite.BusinessObject;
using BetterSite.UI.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Areas.Admin.Controllers
{
     [AuthorizeFilter]
    public class SiteTagController : Controller
    {
        private readonly SiteTagBO sitetagBO = new SiteTagBO();
        //
        // GET: /Admin/SiteTag/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllData(BetterSite.Domain.M_SiteTag where)
        {
           // where.PageIndex = page;
           // where.PageSize = rows;
            var list = sitetagBO.QueryForList(where);
            return Json(list, JsonRequestBehavior.AllowGet);
        }     
      
    }
}
