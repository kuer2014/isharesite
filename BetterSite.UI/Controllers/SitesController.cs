using BetterSite.BusinessObject;
using BetterSite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Controllers
{
    public class SitesController : Controller
    {
        private readonly SitesBO sitesBO=new SitesBO();
      //  private readonly SiteTagBO siteTagBO = new SiteTagBO();
        private readonly TypesBO typesBO = new TypesBO();
       private readonly TagsBO tagsBO = new TagsBO();
       public ActionResult Index(BetterSite.Domain.M_Sites where, string[] TagId)
        {
         //   IList<M_Types> types = typesBO.QueryForEntityList(null);
         //   ViewBag.Types = types;
         // var tags = tagsBO.QueryForList(null).Cast<M_Tags>().ToList();
           //ViewBag.Tags = tags;
            //ViewBag.TypeText = "分类信息";

            where.Sort = where.Sort ?? "SiteAddDate";
            where.Order = where.Order ?? "Asc";
           //根据标签查站点数据
            var count = sitesBO.QueryForList(where).Count;
          //  var list = sitesBO.QueryForPageList(where).Cast<M_Tags>().ToList();
            var list = sitesBO.QueryForList(where).Cast<M_Sites>().ToList();
            return View(list);
        }
        }
}
