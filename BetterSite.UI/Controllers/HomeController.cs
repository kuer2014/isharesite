using BetterSite.BusinessObject;
using BetterSite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly SitesBO sitesBO=new SitesBO();
        public ActionResult Index()
        {
            return View();
        }
        //public IList<M_Sites> QueryForList()
        //{
        //    M_Sites where = null;
        //    return sitesBO.QueryForList(where);
        //}
        public JsonResult GetAllSites()
        {
        
           // Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
          //  timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
            return Json(sitesBO.QueryForList(null), JsonRequestBehavior.AllowGet);
           // string json = JsonConvert.SerializeObject(sitesBO.QueryForList(null), timeConverter);
           // return json;
        }
    }
}
