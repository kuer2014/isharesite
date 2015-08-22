﻿using BetterSite.BusinessObject;
using BetterSite.UI.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Areas.Admin.Controllers
{
     [AuthorizeFilter]
    public class TagsController : Controller
    {
        private readonly TagsBO tagsBO = new TagsBO();
        //
        // GET: /Admin/Types/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllEntitys(BetterSite.Domain.M_Tags where)//, int page, int rows)
        {
            //where.PageIndex = page;
           // where.PageSize = rows;
            var count = tagsBO.QueryForList(where).Count;
            var list = tagsBO.QueryForPageList(where);
            var data = new
            {
                total = count,
                rows = list
            };
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAllData()
        {

            var list = tagsBO.QueryForList(null);
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        //
        // GET: /Admin/Types/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Types/Create

        [HttpPost]
        public JsonResult Create(BetterSite.Domain.M_Tags entity)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                tagsBO.Insert(entity);
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
        public JsonResult Edit(BetterSite.Domain.M_Tags entity)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                tagsBO.Update(entity);
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
        public ActionResult Delete(BetterSite.Domain.M_Tags entity)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                tagsBO.Delete(entity.TagId);
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
