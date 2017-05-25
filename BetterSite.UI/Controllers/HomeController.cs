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
      //  private readonly SiteTagBO siteTagBO = new SiteTagBO();
        private readonly TypesBO typesBO = new TypesBO();
       private readonly TagsBO tagsBO = new TagsBO();
       public ActionResult Index(BetterSite.Domain.M_Sites where)
        {
         //   IList<M_Types> types = typesBO.QueryForEntityList(null);
         //   ViewBag.Types = types;
          var tags = tagsBO.QueryForList(null).Cast<M_Tags>().ToList();
           ViewBag.Tags = tags;
            //ViewBag.TypeText = "分类信息";

            where.Sort = where.Sort ?? "SiteAddDate";
            where.Order = where.Order ?? "Desc";
            where.SiteIsActive = true;
            where.Page = 1;
            where.Rows = 10;
          
          //  var count = sitesBO.QueryForList(where).Count;
         
           //最新收录
           // var listNew = sitesBO.QueryForPageList(where).Cast<M_Sites>().OrderByDescending(s => s.SiteAddDate).ToList();
            var listNew = sitesBO.QueryForStuffTagsPageList(where).Cast<M_Sites>().OrderByDescending(s => s.SiteAddDate).Take(10).ToList();
            ViewBag.New = listNew;
            where.Sort =  "SiteOrderNumber";
            where.Order = "ASC";
            //技术列表
            where.TypeCode = "JS";
            var listJS = sitesBO.QueryForPageList(where).Cast<M_Sites>().ToList();
            ViewBag.JS = listJS;
           //资源列表
            where.TypeCode = "ZY";
            var listZY = sitesBO.QueryForPageList(where).Cast<M_Sites>().ToList();
            ViewBag.ZY = listZY;
            //资讯列表
            where.TypeCode = "ZX";
            var listZX = sitesBO.QueryForPageList(where).Cast<M_Sites>().ToList();
            ViewBag.ZX = listZX;
            //生活列表
            where.TypeCode = "SH";
            var listSH = sitesBO.QueryForPageList(where).Cast<M_Sites>().ToList();
            ViewBag.SH = listSH;
            //默认列表
            //var list = sitesBO.QueryForList(where).Cast<M_Sites>().ToList();
            //var list = sitesBO.QueryForPageList(where).Cast<M_Sites>().ToList();
            where.TypeCode = string.Empty;
            //站长推荐:显示置顶的网站
            where.SiteIsTop = true;
            var listIsTop = sitesBO.QueryForPageList(where).Cast<M_Sites>().OrderByDescending(s => s.SiteAddDate).ToList();
            ViewBag.IsTop = listIsTop;
            //常用站点：显示推到首页的网站
            where.SiteIsTop = false;
            where.SiteIsHome = true;
            var listIsHome = sitesBO.QueryForPageList(where).Cast<M_Sites>().OrderByDescending(s => s.SiteAddDate).ToList();
            ViewBag.IsHome = listIsHome;
           // return View(list);
            return View();
        }
       #region demo_knockout
       public ActionResult _knockoutIndex()
        {
            ViewBag.TypeText = "分类信息";
            return View();
        }
        //public IList<M_Sites> QueryForList()
        //{
        //    M_Sites where = null;
        //    return sitesBO.QueryForList(where);
        //}
        //public JsonResult _knockoutGetAllSites()
        //{
        
        //   // Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        //  //  timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        //    return Json(sitesBO.QueryForList(null), JsonRequestBehavior.AllowGet);
        //   // string json = JsonConvert.SerializeObject(sitesBO.QueryForList(null), timeConverter);
        //   // return json;
        //}
        #endregion demo_knockout
    }
}
