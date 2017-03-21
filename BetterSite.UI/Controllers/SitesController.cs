using BetterSite.BusinessObject;
using BetterSite.Domain;
using System;
using System.Collections;
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
            where.SiteIsActive = true;
            #region 根据标签查找对应的站点Id
            var m_SiteTag = new M_SiteTag();
            if (TagId != null && TagId.Count() > 0)
            {
                string tagsId=string.Join("','", TagId);
                m_SiteTag.TagId = tagsId;
                /// 标签关系[或]
                //IList<M_SiteTag> tags = siteTagBO.QueryForList(m_SiteTag).Cast<M_SiteTag>().ToList();
                //where.SiteId = string.Join("','", tags.Select(s => s.SiteId));
                ///标签关系[或]end
                
                ///标签关系[且]start
                Hashtable htTagsId = new Hashtable();
                htTagsId.Add("TagId", tagsId);
                htTagsId.Add("TagCount", TagId.Count());
                IList<M_SiteTag> tags = siteTagBO.QueryForListByTags(htTagsId).Cast<M_SiteTag>().ToList();
               
                if (tags.Count == 0) {
                    where.SiteId = Guid.NewGuid().ToString();
                }
                else { 
                where.SiteId = string.Join("','", tags.Select(s => s.SiteId));
                }
                ///标签关系[且]end
            }
            #endregion
            ////var count = sitesBO.QueryForList(where).Count;
            ////  var list = sitesBO.QueryForPageList(where).Cast<M_Tags>().ToList();
            var list = sitesBO.QueryForStuffTagsList(where).Cast<M_Sites>();
            //var list = sitesBO.QueryForJoinTagList(where).Cast<M_Sites>().ToList();  
            var types=typesBO.QueryForEntityList(new M_Types { TypeCode = where.TypeCode });
            if (types.Count > 0)
            { ViewBag.Title = types[0].TypeName + "-优站分享-为开发者提供一些便利"; }
            return View(list);
        }
        /// <summary>
        /// [测试group by]站点数据，条件为TypeCode 和TagId
        /// </summary>
        /// <param name="where">包含了TypeCode</param>
        /// <param name="TagId">页面已勾选的标签</param>
        /// <returns>站点数据</returns>
        public ActionResult ListGroup(BetterSite.Domain.M_Sites where, string[] TagId)
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
            ///不分组
            //var list = sitesBO.QueryForJoinTagList(where).Cast<M_Sites>().ToList();    
            ///分组方式 （对应前台按顺序从上到下）
            //第一种（MSDN示例）
            //var list = sitesBO.QueryForJoinTagList(where).Cast<M_Sites>().GroupBy(s => s.SiteName, s => s.TagName);//.OrderBy(f => f.Key);
            //第二种
           // var list = sitesBO.QueryForJoinTagList(where).Cast<M_Sites>().GroupBy(s => s.Site, s => s.TagName);//.OrderBy(f => f.Key.SiteName); 
            //第三种（以下两个效果相同）
            //var list = sitesBO.QueryForJoinTagList(where).Cast<M_Sites>().GroupBy(s => s.Site);//.OrderBy(f => f.Key.SiteName);
            var list = sitesBO.QueryForJoinTagList(where).Cast<M_Sites>().GroupBy(s => new M_Sites { SiteName = s.SiteName, SiteUrl = s.SiteUrl }, s => new M_Sites { TagName = s.TagName });//.OrderBy(f => f.Key.SiteName); ;

            return View(list);
        }
    }
}
