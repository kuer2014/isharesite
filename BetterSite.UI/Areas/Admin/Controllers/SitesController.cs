﻿using BetterSite.BusinessObject;
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
        private readonly SiteTagBO siteTagBO = new SiteTagBO();
        //
        // GET: /Admin/Sites/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllEntitys(BetterSite.Domain.M_Sites where, int page, int rows)
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
        // POST: /Admin/Sites/Create
        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(M_Sites entity,string [] TagId)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                sitesBO.Insert(entity);
                //插入标签信息
                for (int i = 0; i < TagId.Length; i++)
                {
                    siteTagBO.Insert(new M_SiteTag { SiteId = entity.SiteId, TagId = TagId[i] });
                }               
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
        // POST: /Admin/Sites/Edit/5

        [HttpPost]
        public ActionResult Edit(M_Sites entity, string[] TagId)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                sitesBO.Update(entity);
                //修改标签信息（先根据siteId删除，再插入）
                siteTagBO.DeleteBySiteId(entity.SiteId);
                for (int i = 0; i < TagId.Length; i++)
                {
                    siteTagBO.Insert(new M_SiteTag { SiteId = entity.SiteId, TagId = TagId[i] });
                }               
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
        // POST: /Admin/Sites/Delete/5

        [HttpPost]
        public ActionResult Delete(M_Sites entity)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                sitesBO.Delete(entity.SiteId);
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
