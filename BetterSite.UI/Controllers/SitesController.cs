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
            if (Tag != null && Tag.Count() > 0)
            {
                string tagsName = string.Join("','", Tag);           
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
            }
            #endregion          
         
         
            //标题
            string title = "优站分享|致力于分享实用的优秀网站";
            string keywords = "";
            if (string.IsNullOrWhiteSpace(where.TypeCode))
            {
                title = "全部 - " + title;
                keywords = "优站分享,网站分享,网站推荐,免费素材,在线工具,发现好玩,便民查询,个人提升,行业专栏";
            }
            else if (where.TypeCode.ToLower() == "gengxin")
            {
                title = "最新收录 - " + title;
                keywords = "最新收录,优站分享,网站分享,网站推荐";
                where.TypeCode = "";
            }
            else
            {
                var types = typesBO.QueryForEntityList(new M_Types { TypeCode = where.TypeCode });
                if (types.Count > 0)
                {
                    title = types[0].TypeName + " - " + title;
                    keywords = types[0].TypeName + ",优站分享,网站分享,网站推荐";
                }
                //else {
                //    return Redirect("/sites");
                //}               
            }
            ViewBag.Title = title;
            ViewBag.Keywords = keywords;
            ViewBag.Description = "优站分享,致力于分享实用的优秀网站。分享网站涵盖免费素材,在线工具,发现好玩,便民查询,个人提升,行业专栏等,优站分享正努力成为您工作、学习、生活的好帮手。";
        // ViewData["TypeCode"] = where.TypeCode;
       // TempData["TypeCode"] = where.TypeCode;//跨控制器
            int pagesize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["pagesize"]);
            where.Rows = pagesize;
            where.Page = where.Page == 0 ? where.Page = 1 : where.Page;
            var list = sitesBO.QueryForStuffTagsPageList(where).Cast<M_Sites>().OrderByDescending(s => s.SiteAddDate).ToList();
            var listCount = sitesBO.QueryForStuffTagsList(where).Cast<M_Sites>().Count();
            ViewBag.ListCount = listCount;
            ViewBag.Page = where.Page;
            ViewBag.PageCount = (int)Math.Ceiling(Convert.ToDouble(listCount) / Convert.ToDouble(pagesize)); ;
            return View(list);
        }
        // GET /Sites/SITE1489992926300
        public ActionResult Detail(string SiteCode) {
            var where = new M_Sites() { SiteCode = SiteCode,SiteIsActive =true };
            var model = sitesBO.QueryForStuffTagsList(where).Cast<M_Sites>().FirstOrDefault();
            if (model != null)
            {
                ViewBag.Title = model.SiteName + " - 优站分享|致力于分享实用的优秀网站";
                ViewBag.Keywords = model.SiteTagsName+","+model.TypeName+",优站分享";
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
