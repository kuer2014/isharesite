using BetterSite.BusinessObject;
using BetterSite.Domain;
using Newtonsoft.Json;
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
        [HttpPost]
        [ValidateInput(false)]
        public string Add(string entityJson, string token)
        {
            if (token != "2CBa31gg4s7dB")
            {
                return "非法操作!";
            }
            List<M_Article> entityList = JsonConvert.DeserializeObject<List<M_Article>>(entityJson);
            string msg = string.Empty;
            try
            {
                foreach (var entity in entityList)
                {
                    if (!string.IsNullOrWhiteSpace(entity.Title)
                       && !string.IsNullOrWhiteSpace(entity.Content))
                    {
                        entity.Content = SubStr(entity.Content);
                        entity.Description = entity.Content.Length > 100 ? entity.Content.Substring(0, 150) : entity.Content;
                        entity.Description = entity.Description.Replace("<p>", "").Replace("</p>", "");
                        entity.Category = 3;//热点
                        entity.Status = 3;// 3 / 导入
                        var r = articleBO.Insert(entity);
                        //int num = 0;
                        //int.TryParse(r+"", out num);
                        //msg = num + "";
                    }
                }
            }
            catch (Exception ex)
            {

                msg = ex.Message;

            }
            return msg;
        }
        /// <summary>
        /// 去掉括号和括号内的内容，只去首次出现的
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string SubStr(string txt) {
            //string txt = "<p>（本报美里26日讯）拿督）格拉（瓦表示﹐为了）应付多年来的交通";
            txt = txt.Replace("（", "(").Replace("）", ")");
            int n = txt.IndexOf("(");
            int m = txt.IndexOf(")");
            if (n > 0 && m > n) { 
                string _txt = txt.Substring(n, m - n + 1);
                txt = txt.Replace(_txt, "");  //string ntxt = "<p>拿督）格拉（瓦表示﹐为了）应付多年来的交通";
            }            
            return txt;
        }
    }
}
