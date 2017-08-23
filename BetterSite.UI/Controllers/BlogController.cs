using BetterSite.BusinessObject;
using BetterSite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/
        private readonly BlogBO blogBO = new BlogBO();
        public ActionResult Index(BetterSite.Domain.M_Blog where)
        {
            int pagesize = 300;// int.Parse(System.Configuration.ConfigurationManager.AppSettings["articlepagesize"]);
            where.Status = 1;
            where.Category = null;
            where.Rows = pagesize;
            where.Page = where.Page == 0 ? where.Page = 1 : where.Page;
            var list = blogBO.QueryForPageList(where).Cast<M_Blog>().ToList();
            var listCount = blogBO.QueryForList(where).Cast<M_Blog>().Count();
            ViewBag.MonthPlan = list.Where(w => w.Category == 3).OrderByDescending(o => o.Id).FirstOrDefault();
            ViewBag.YearPlan= list.Where(w => w.Category == 4).OrderByDescending(o => o.Id).FirstOrDefault();
            ViewBag.ListCount = listCount;
            ViewBag.Page = where.Page;
            ViewBag.PageCount = (int)Math.Ceiling(Convert.ToDouble(listCount) / Convert.ToDouble(pagesize));
            return View(list);
        }

        //
        // GET: /Blog/Details/5

        public ActionResult Details(int id)
        {
            var modelList = blogBO.QueryForEntityList(new M_Blog { Id = id, Status = 1 });
            var model = modelList.Where(m => m.Id == id).FirstOrDefault();
            if (model != null)
            {
                
                string desc = model.Description;
                if (!string.IsNullOrWhiteSpace(desc) && desc.Length > 100)
                {
                    desc = desc.Substring(0, 100) + "...";
                }
                ViewBag.Description = desc;
                ///同类文章
                var listType = blogBO.QueryForPageList(new M_Blog { Category = model.Category, Status = 1, Rows = 10, Page = 1 });
                ViewBag.ListType = listType;
                //加载评论
                ViewBag.CommentList = blogBO.QueryBlogCommentForList(new M_BlogComment { BlogId = model.Id, Status = 1 });
                //上一条，下一条
                ViewBag.Pre = modelList.Where(m => m.Id < id).FirstOrDefault();
                ViewBag.Next = modelList.Where(m => m.Id > id).FirstOrDefault();
                //更新点击数
                blogBO.UpdateBlogClickQuantity(model.Id);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Blog/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Blog/Create

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
        // GET: /Blog/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Blog/Edit/5

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
        // GET: /Blog/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Blog/Delete/5

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
        public ActionResult About()
        {
            return View();
        }
    }
}
