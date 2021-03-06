﻿using BetterSite.BusinessObject;
using BetterSite.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Controllers
{

    public class SiteApiController : Controller
    {
        private readonly SitesBO sitesBO = new SitesBO();
        //
        // GET: /SiteApi/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /SiteApi/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SiteApi/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SiteApi/Create

        [HttpPost]
        [ValidateInput(false)]
        public string Add(string entityJson, string token, string sitetags)
        {
            if (token != "2CBa31gg4s7dB") {
                return "非法操作!";
            }
            M_Sites entity = JsonConvert.DeserializeObject<M_Sites>(entityJson);
            string msg = string.Empty;
            try
            {
                if (!string.IsNullOrWhiteSpace(entity.SiteCode)
                    && !string.IsNullOrWhiteSpace(entity.SiteName)
                    && !string.IsNullOrWhiteSpace(entity.SiteUrl)
                    && !string.IsNullOrWhiteSpace(entity.TypeId)
                    && !string.IsNullOrWhiteSpace(entity.SiteProfile)
                     && entity.SiteOrderNumber > 0
                     &&!string.IsNullOrWhiteSpace(entity.SiteImgBase64))
                {
                    ImageHelper.Base64ToFile(entity.SiteImgBase64.Replace('-', '+').Replace('_', '/'), Server.MapPath("~/Images/SiteScreen"), entity.SiteCode);
                    entity.SiteId = Guid.NewGuid().ToString();
                    entity.SiteCollectionDate = DateTime.Now;
                 //   entity.SiteProfile = System.Web.HttpUtility.UrlDecode(entity.SiteProfile);
                    var result = sitesBO.Insert(entity);  //(SiteId,SiteCode,SiteName,SiteUrl,TypeId,SiteOrderNumber,SiteCollectionDate,SiteProfile)
                    if (!string.IsNullOrWhiteSpace(sitetags))
                    {
                        //插入标签信息
                        BusinessObject.SiteTagBO siteTagBO = new SiteTagBO();
                        var tagsId = sitetags.Split(',');
                        for (int i = 0; i < tagsId.Length; i++)
                        {
                            if(!string.IsNullOrWhiteSpace(tagsId[i]))
                            siteTagBO.Insert(new M_SiteTag { SiteId = entity.SiteId, TagId = tagsId[i] });
                        }

                       
                    }
                    msg = "添加成功";
                    //// else
                    // {
                    //     msg = "添加失败";
                    // }
                }
                else
                {
                    msg = $"参数有误:SiteCode:{entity.SiteCode};SiteName:{entity.SiteName};SiteUrl:{entity.SiteUrl};TypeId:{entity.TypeId};SiteProfile:{entity.SiteProfile};SiteOrderNumber:{entity.SiteOrderNumber}。";
                }
            }
            catch (Exception ex)
            {

                msg = ex.Message;

            }
            return msg;
        }
        [HttpPost]
        public string GetTags(string typeId, string token)
        {
            if (token != "2CBa31gg4s7dB")
            {
                return "非法操作!";
            }           
            string msg = string.Empty;
            try
            {
                TagsBO tagsBO = new TagsBO();
                var taglist = tagsBO.QueryForEntityListByTypeId(typeId);
                return JsonConvert.SerializeObject(taglist);
            }
            catch (Exception ex)
            {

                msg = ex.Message;

            }
            return msg;
        }

        //
        // GET: /SiteApi/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SiteApi/Edit/5

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
        // GET: /SiteApi/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SiteApi/Delete/5

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
