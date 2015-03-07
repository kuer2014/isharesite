﻿using BetterSite.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Areas.Admin.Controllers
{
    public class TypesController : Controller
    {
        private readonly TypesBO typesBO = new TypesBO();
        //
        // GET: /Admin/Types/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllSites(BetterSite.Domain.M_Types where, int page, int rows)
        {
            where.PageIndex = page;
            where.PageSize = rows;
            var count = typesBO.QueryForList(where).Count;
            var list = typesBO.QueryForPageList(where);
            var data = new
            {
                total = count,
                rows = list
            };
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        //
        // GET: /Admin/Types/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Types/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Types/Create

        [HttpPost]
        public JsonResult Create(BetterSite.Domain.M_Types entity)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                typesBO.Insert(entity);
                json.Data = new
                {
                    result = true,
                    msg = "添加成功"
                };         
               
            }
            catch (Exception ex)
            {
                json.Data = new
                {
                    result = false,
                    msg = ex.Message
                };         
            }
            return json;
        }

        //
        // GET: /Admin/Types/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Types/Edit/5

        [HttpPost]
        public JsonResult Edit(BetterSite.Domain.M_Types entity)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                typesBO.Update(entity);
                json.Data = new
                {
                    result = true,
                    msg = "修改成功"
                };

            }
            catch (Exception ex)
            {
                json.Data = new
                {
                    result = false,
                    msg = ex.Message
                };
            }
            return json;
        }

        //
        // GET: /Admin/Types/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Types/Delete/5

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
