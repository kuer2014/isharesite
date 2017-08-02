using BetterSite.BusinessObject;
using BetterSite.UI.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Areas.Admin.Controllers
{
    [AuthorizeFilter]
    public class BlogController : Controller
    {
        private readonly BlogBO articleBO = new BlogBO();
        //
        // GET: /Admin/Blog/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllEntitys(BetterSite.Domain.M_Blog where)//, int page, int rows)
        {
            //  where.PageIndex = page;
            // where.PageSize = rows;
            where.Status = 1;
            where.Category = null;
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
        // GET: /Admin/Blog/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(BetterSite.Domain.M_Blog entity)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
              //  entity.Content = System.Web.HttpUtility.UrlDecode(entity.Content);
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
        [ValidateInput(false)]
        public JsonResult Edit(BetterSite.Domain.M_Blog entity)
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
        public ActionResult Delete(BetterSite.Domain.M_Blog entity)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
               // articleBO.Delete(entity.Id);
               //逻辑删除，修改状态
                entity.Status = 0;                
                articleBO.UpdateStatus(entity);
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
