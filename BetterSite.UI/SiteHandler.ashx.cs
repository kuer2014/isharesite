using BetterSite.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterSite.UI
{
    /// <summary>
    /// SiteHandler 的摘要说明
    /// </summary>
    public class SiteHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = string.Empty;
            string entityJson = context.Request.Form["entityJson"];
            string token = context.Request.Form["token"];
           // string imgbase64 = context.Request.Form["imgbase64"];
            if (token != "2CBa31gg4s7dB")
            {
                result = "非法操作!";
            }
            else
            {
                try
                {
                    M_Sites entity = JsonConvert.DeserializeObject<M_Sites>(entityJson);
                    if (!string.IsNullOrWhiteSpace(entity.SiteCode)
                        && !string.IsNullOrWhiteSpace(entity.SiteName)
                        && !string.IsNullOrWhiteSpace(entity.SiteUrl)
                        && !string.IsNullOrWhiteSpace(entity.TypeId)
                        && !string.IsNullOrWhiteSpace(entity.SiteProfile)
                         && entity.SiteOrderNumber > 0
                         && !string.IsNullOrWhiteSpace(entity.SiteImgBase64))
                    {
                        ImageHelper.Base64ToFile(entity.SiteImgBase64, context.Server.MapPath("~/Images/SiteScreen"), entity.SiteCode);
                        entity.SiteId = Guid.NewGuid().ToString();
                        entity.SiteCollectionDate = DateTime.Now;
                        // var result = sitesBO.Insert(entity);  //(SiteId,SiteCode,SiteName,SiteUrl,TypeId,SiteOrderNumber,SiteCollectionDate,SiteProfile)
                        //if (result != null && (int)result > 0)
                        // {
                        ////插入标签信息
                        //for (int i = 0; i < TagId.Length; i++)
                        //{
                        //    siteTagBO.Insert(new M_SiteTag { SiteId = entity.SiteId, TagId = TagId[i] });
                        //}

                        result = "添加成功";
                        // }
                        //// else
                        // {
                        //     msg = "添加失败";
                        // }
                    }
                    else
                    {
                        result = $"参数有误:SiteCode:{entity.SiteCode};SiteName:{entity.SiteName};SiteUrl:{entity.SiteUrl};TypeId:{entity.TypeId};SiteProfile:{entity.SiteProfile};SiteOrderNumber:{entity.SiteOrderNumber}。";
                    }
                }
                catch (Exception ex)
                {

                    result = ex.Message;

                }
            }
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}