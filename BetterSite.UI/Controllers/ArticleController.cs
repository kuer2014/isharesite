using BetterSite.BusinessObject;
using BetterSite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ArticleBO articleBO = new ArticleBO();
        string title = System.Configuration.ConfigurationManager.AppSettings["title"];
        string keywords = System.Configuration.ConfigurationManager.AppSettings["keywords"];
        string description = System.Configuration.ConfigurationManager.AppSettings["description"];
        //
        // GET: /Article/

        public ActionResult Index(BetterSite.Domain.M_Article where)
        {
            ViewBag.Title = "文章 - " + title;
            ViewBag.Keywords = keywords;
            ViewBag.Description = description;
            int pagesize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["articlepagesize"]);
            where.Status = 1;
           where.Category = null;
            where.Rows = pagesize;
            where.Page = where.Page == 0 ? where.Page = 1 : where.Page;
            var list = articleBO.QueryForPageList (where).Cast<M_Article>().ToList();
            var listCount = articleBO.QueryForList(where).Cast<M_Article>().Count();
            ViewBag.ListCount = listCount;
            ViewBag.Page = where.Page;
            ViewBag.PageCount = (int)Math.Ceiling(Convert.ToDouble(listCount) / Convert.ToDouble(pagesize));
            return View(list);
        }

        //
        // GET: /Article/Details/5

        public ActionResult Details(int id)
        {
            var modelList = articleBO.QueryForEntityList(new M_Article { Id = id, Status = 1 });
           var model= modelList.Where(m=>m.Id==id).FirstOrDefault();
            if (model != null)
            {
                ViewBag.Title = model.Title + " - " + title;
                ViewBag.Keywords = model.Title + ",优站分享";
                string desc = model.Description;
                if (!string.IsNullOrWhiteSpace(desc) && desc.Length > 100)
                {
                    desc = desc.Substring(0, 100) + "...";
                }
                ViewBag.Description = desc;
                ///同类文章
                var listType = articleBO.QueryForPageList(new M_Article { Category = model.Category,Status=1,Rows=10,Page=1 });
            ViewBag.ListType = listType;
            //加载评论
            ViewBag.CommentList = articleBO.QueryArticleCommentForList(new M_ArticleComment { ArticleId = model.Id, Status = 1 });
                //上一条，下一条
                ViewBag.Pre= modelList.Where(m => m.Id < id).FirstOrDefault();
                ViewBag.Next = modelList.Where(m => m.Id > id).FirstOrDefault();
                //更新点击数
                articleBO.UpdateArticleClickQuantity(model.Id);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Article/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Article/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Article/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Article/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Article/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Article/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public JsonResult AddArticleComment(int articleId, string nickname, string commentContent)
        {
            JsonResult json = new JsonResult();
            if (articleId>0 && !string.IsNullOrWhiteSpace(nickname) && !string.IsNullOrWhiteSpace(commentContent))
            {
                try
                {
                    // TODO: Add insert logic here
                    M_ArticleComment entity = new M_ArticleComment();
                    //entity.Id = Guid.NewGuid().ToString().ToUpper();
                    entity.ArticleId = articleId;
                    entity.CreateTime = DateTime.Now;
                    entity.CommentUserNickname = nickname;
                    entity.CommentUserIp = System.Web.HttpContext.Current.Request.UserHostAddress;
                    entity.CommentContent = commentContent;
                    entity.Status = 2;//待审核
                    articleBO.AddArticleComment(entity);

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
            else
            {
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
