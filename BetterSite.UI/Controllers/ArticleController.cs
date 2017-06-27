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
            int pagesize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["pagesize"]);
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
           var model= articleBO.QueryForEntityList(new M_Article { Id = id }).FirstOrDefault();
            var listType = articleBO.QueryForPageList(new M_Article { Category = model.Category,Status=1,Rows=10,Page=1 });
            ViewBag.ListType = listType;
            return View(model);
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
    }
}
