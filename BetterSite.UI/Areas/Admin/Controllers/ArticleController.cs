using BetterSite.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ArticleBO articleBO = new ArticleBO();
        //
        // GET: /Admin/Article/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllEntitys(BetterSite.Domain.M_Article where)//, int page, int rows)
        {
            //  where.PageIndex = page;
            // where.PageSize = rows;
            var count = articleBO.QueryForList(where).Count;
            var list = articleBO.QueryForPageList(where);
            var data = new
            {
                total = count,
                rows = list
            };
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        //
        // GET: /Admin/Article/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(BetterSite.Domain.M_Article entity)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                articleBO.Insert(entity);
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
            return json;
        }

        //
        // POST: /Admin/Types/Edit/5

        [HttpPost]
        public JsonResult Edit(BetterSite.Domain.M_Article entity)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                articleBO.Update(entity);
                json.Data = new
                {
                    success = true,
                    msg = "修改成功"
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
            return json;
        }

        //
        // POST: /Admin/Types/Delete/5

        [HttpPost]
        public ActionResult Delete(BetterSite.Domain.M_Article entity)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                articleBO.Delete(entity.Id);
                json.Data = new
                {
                    success = true,
                    msg = "删除成功"
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
            return json;
        }
    }
}
