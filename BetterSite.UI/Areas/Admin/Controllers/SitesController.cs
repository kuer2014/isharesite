using BetterSite.BusinessObject;
using BetterSite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Areas.Admin.Controllers
{
    public class SitesController : Controller
    {
        private readonly SitesBO sitesBO = new SitesBO();
        //
        // GET: /Admin/Sites/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllSites(BetterSite.Domain.M_Sites where, int page, int rows)
        {
            where.PageIndex = page;
            where.PageSize = rows;
            var count = sitesBO.QueryForList(where).Count;
            var list = sitesBO.QueryForPageList(where);
            var data = new
            {
                total = count,
                rows = list
            };
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        //
        // GET: /Admin/Sites/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Sites/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Sites/Create
        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(M_Sites entity)
        {
            try
            {
                // TODO: Add insert logic here
               //   var entity = new M_Sites();  
          //在这里转换  
                 // TryUpdateModel<M_Sites>(entity, collection);
                  sitesBO.Insert(entity);
                  return Json(entity, JsonRequestBehavior.AllowGet);
              //  return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                var err = new
                {
                   errmsg=ex.Message
                };
                return Json(err, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Admin/Sites/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Sites/Edit/5

        [HttpPost]
        public ActionResult Edit(M_Sites entity)
        {
            try
            {
                // TODO: Add update logic here

                sitesBO.Update(entity);
                return Json(entity, JsonRequestBehavior.AllowGet);
                //  return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var err = new
                {
                    errmsg = ex.Message
                };
                return Json(err, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Admin/Sites/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Sites/Delete/5

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
        public JsonResult _knockoutGetAllSites(M_Sites where)
        {

            // Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //  timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
            // string json = JsonConvert.SerializeObject(sitesBO.QueryForList(null), timeConverter);
            // return json;
            var count = sitesBO.QueryForList(where).Count;
            var list = sitesBO.QueryForPageList(where);
            var data = new
            {
                count = count,
                list = list
            };
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}
