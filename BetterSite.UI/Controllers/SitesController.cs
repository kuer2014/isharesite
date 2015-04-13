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
        private readonly SitesBO sitesBO = new SitesBO();
        private readonly SiteTagBO siteTagBO = new SiteTagBO();
        private readonly TypesBO typesBO = new TypesBO();
        private readonly TagsBO tagsBO = new TagsBO();
        /// <summary>
        /// 站点数据，条件为TypeCode 和TagId
        /// </summary>
        /// <param name="where">包含了TypeCode</param>
        /// <param name="TagId">页面已勾选的标签</param>
        /// <returns>站点数据</returns>
        public ActionResult Index(BetterSite.Domain.M_Sites where, string[] TagId)
        {
            where.Sort = where.Sort ?? "SiteAddDate";
            where.Order = where.Order ?? "Asc";
            #region 根据标签查找对应的站点Id
            var m_SiteTag = new M_SiteTag();
            if (TagId != null && TagId.Count() > 0)
            {
                m_SiteTag.TagId = string.Join("','", TagId);
                IList<M_SiteTag> tags = siteTagBO.QueryForList(m_SiteTag).Cast<M_SiteTag>().ToList();
                where.SiteId = string.Join("','", tags.Select(s => s.SiteId));
            }
            #endregion
            var count = sitesBO.QueryForList(where).Count;
            //  var list = sitesBO.QueryForPageList(where).Cast<M_Tags>().ToList();
            var list = sitesBO.QueryForList(where).Cast<M_Sites>().ToList();
            return View(list);
        }
    }
}
