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
        public ActionResult Index(BetterSite.Domain.M_Sites where, string[] Tag)
        {
            where.Sort = where.Sort ?? "SiteAddDate";
            where.Order = where.Order ?? "Desc";
            where.SiteIsActive = true;
            #region 根据标签Name查找对应的站点Id
           // var m_SiteTag = new M_SiteTag();
            if (Tag != null && Tag.Count() > 0)
            {
                string tagsName = string.Join("','", Tag);
               // m_SiteTag.TagId = tagsId;
                /// 标签关系[或]
                //IList<M_SiteTag> tags = siteTagBO.QueryForList(m_SiteTag).Cast<M_SiteTag>().ToList();
                //where.SiteId = string.Join("','", tags.Select(s => s.SiteId));
                ///标签关系[或]end

                ///标签关系[且]start
                Hashtable htTagsId = new Hashtable();
                htTagsId.Add("TagsName", tagsName);
                htTagsId.Add("TagCount", Tag.Count());
                IList<M_SiteTag> tags = siteTagBO.QueryForListByTags(htTagsId).Cast<M_SiteTag>().ToList();

                if (tags.Count == 0)
                {
                    where.SiteId = Guid.NewGuid().ToString();
                }
                else
                {
                    where.SiteId = string.Join("','", tags.Select(s => s.SiteId));
                }
                ///标签关系[且]end
            }
            #endregion
            #region 根据标签Id查找对应的站点Id
            //var m_SiteTag = new M_SiteTag();
            //if (TagId != null && TagId.Count() > 0)
            //{
            //    string tagsId=string.Join("','", TagId);
            //    m_SiteTag.TagId = tagsId;
            //    /// 标签关系[或]
            //    //IList<M_SiteTag> tags = siteTagBO.QueryForList(m_SiteTag).Cast<M_SiteTag>().ToList();
            //    //where.SiteId = string.Join("','", tags.Select(s => s.SiteId));
            //    ///标签关系[或]end
                
            //    ///标签关系[且]start
            //    Hashtable htTagsId = new Hashtable();
            //    htTagsId.Add("TagsId", tagsId);
            //    htTagsId.Add("TagCount", TagId.Count());
            //    IList<M_SiteTag> tags = siteTagBO.QueryForListByTags(htTagsId).Cast<M_SiteTag>().ToList();
               
            //    if (tags.Count == 0) {
            //        where.SiteId = Guid.NewGuid().ToString();
            //    }
            //    else { 
            //    where.SiteId = string.Join("','", tags.Select(s => s.SiteId));
            //    }
            //    ///标签关系[且]end
            //}
            #endregion
            ////var count = sitesBO.QueryForList(where).Count;
            ////  var list = sitesBO.QueryForPageList(where).Cast<M_Tags>().ToList();
            var list = sitesBO.QueryForStuffTagsList(where).Cast<M_Sites>().OrderByDescending(s => s.SiteAddDate).ToList();
            //var list = sitesBO.QueryForJoinTagList(where).Cast<M_Sites>().ToList();  
            //标题
            string title = "优站分享|致力于分享实用的优秀网站";
            if (string.IsNullOrWhiteSpace(where.TypeCode))
            {
                title = "全部-"+ title;
            }
            else
            {
                var types = typesBO.QueryForEntityList(new M_Types { TypeCode = where.TypeCode });
                if (types.Count > 0)
                {
                    title = types[0].TypeName + "-" + title;
                }
                else {
                    return Redirect("/Sites");
                }               
            }
            ViewBag.Title = title;
           // ViewData["TypeCode"] = where.TypeCode;
            TempData["TypeCode"] = where.TypeCode;//跨控制器
            return View(list);
        }
        // GET /Sites/SITE1489992926300
        public ActionResult Detail(string SiteCode) {
            var where = new M_Sites() { SiteCode = SiteCode,SiteIsActive =true };
            var model = sitesBO.QueryForStuffTagsList(where).Cast<M_Sites>().FirstOrDefault();
            if (model != null)
            {
                ViewBag.Title = model.SiteName + " 优站分享|致力于分享实用的优秀网站";
                ViewBag.Keywords = model.SiteName;
                ViewBag.Description = model.SiteName+","+model.SiteProfile;
                //加载评论
                ViewBag.CommentList=sitesBO.QuerySiteCommentForList(new M_SiteComment {SiteId= model.SiteId,Status=1 });
                //同类站点 和常用站点
                BetterSite.Domain.M_Sites siteWhere = new M_Sites();
                siteWhere.SiteIsActive = true;
                siteWhere.Page = 1;
                siteWhere.Rows = 10;
                siteWhere.Sort = "SiteOrderNumber";
                siteWhere.Order = "ASC";
                //同类站点
                siteWhere.TypeCode =model.TypeCode;
                var listType = sitesBO.QueryForPageList(siteWhere).Cast<M_Sites>().ToList();
                ViewBag.ListType = listType;
                //siteWhere.TypeCode = "";
                ////站长推荐:显示置顶的网站
                //siteWhere.SiteIsTop = true;
                //var listIsTop = sitesBO.QueryForPageList(siteWhere).Cast<M_Sites>().OrderByDescending(s => s.SiteAddDate).ToList();
                //ViewBag.IsTop = listIsTop;
                ////常用站点：显示推到首页的网站
                //siteWhere.SiteIsTop = false;
                //siteWhere.SiteIsHome = true;
                //var listIsHome = sitesBO.QueryForPageList(siteWhere).Cast<M_Sites>().OrderByDescending(s => s.SiteAddDate).ToList();
                //ViewBag.IsHome = listIsHome;
                return View(model);
            }
            else {               
               return RedirectToAction("Index");
            }
         
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
       [HttpPost]
        public JsonResult AddSiteComment(string siteId,string nickname, string commentContent)
        {
            JsonResult json = new JsonResult();
            if (!string.IsNullOrWhiteSpace(siteId) && !string.IsNullOrWhiteSpace(nickname) && !string.IsNullOrWhiteSpace(commentContent))
            {
                try
                {
                    // TODO: Add insert logic here
                    M_SiteComment entity = new M_SiteComment();
                    entity.Id = Guid.NewGuid().ToString().ToUpper();
                    entity.SiteId = siteId;
                    entity.CreateTime = DateTime.Now;
                    entity.CommentUserNickname = nickname;
                    entity.CommentUserIp = System.Web.HttpContext.Current.Request.UserHostAddress;
                    entity.CommentContent = commentContent;
                    entity.Status = 2;//待审核
                    sitesBO.AddSiteComment(entity);

                    json.Data = new
                    {
                        success = true,
                        msg = "添加成功"
                    };
                }
                catch (Exception ex)
                {
                    json.Data = new
                    {
                        success = false,
                        msg = ex.Message
                    };
                }
            }
            else {
                json.Data = new
                {
                    success = false,
                    msg = "参数有误"
                };
            }
            //json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }
    }
}
