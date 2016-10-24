using BetterSite.BusinessObject;
using BetterSite.Domain;
using BetterSite.UI.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Areas.Admin.Controllers
{
    //[CustomAuth(false)]
      [AuthorizeFilter]
    public class SitesController : Controller
    {
        private readonly SitesBO sitesBO = new SitesBO();
        private readonly SiteTagBO siteTagBO = new SiteTagBO();
        private readonly TypesBO typesBO = new TypesBO();
        //
        // GET: /Admin/Sites/

        public ActionResult Index()
        {
            return View();
        }
         // [AuthorizeFilter]
        public JsonResult GetAllEntitys(BetterSite.Domain.M_Sites where)//, int page, int rows, string sort, string order)
        {
          //  where.PageIndex = page;
          //  where.PageSize = rows;
       //   where.Sort=  string.IsNullOrWhiteSpace(where.Sort) ? "SiteAddDate" : where.Sort;
          where.Sort = where.Sort?? "SiteAddDate";
          where.Order = where.Order ?? "Asc";

          where.SiteIsActive = true;

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
                siteTagBO.DeleteBySiteId(string.Format("'{0}'",entity.SiteId));
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
        // POST: /Admin/Sites/Delete/5

        [HttpPost]
        public ActionResult ToTop(string sitesId,int isTop)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                sitesBO.ToTop(sitesId,isTop);
              
                json.Data = new
                {
                    success = true,
                    msg = "操作成功"
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
        // POST: /Admin/Sites/Delete/5

        [HttpPost]
        public ActionResult ToHome(string sitesId,int isHome)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                sitesBO.ToHome(sitesId,isHome);
              
                json.Data = new
                {
                    success = true,
                    msg = "操作成功"
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
        public ActionResult Delete(string sitesId)
        {
            JsonResult json = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                sitesBO.Delete(sitesId);
                //删除站点标签关系
                siteTagBO.DeleteBySiteId(sitesId);
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
        public ActionResult Import()
        {
            return View();
        }
         [HttpPost]
         [ValidateInput(false)]
        public JsonResult Import(string sitesJson)
        {
            JsonResult json = new JsonResult();
            int importCount = 0;
            int successCount = 0;
            try
            {
                var sites = Newtonsoft.Json.JsonConvert.DeserializeObject<List<M_Sites>>(sitesJson);
                importCount = sites.Count;
                //处理类型
                var typeId = string.Empty;
                IList<M_Types> types = typesBO.QueryForEntityList(new BetterSite.Domain.M_Types() { TypeCode = "DR" });
                // types = (typesBO.QueryForList(new BetterSite.Domain.M_Types() { TypeCode = "DR" })) as System.Collections.ArrayList;
				// IList<M_Types> types = typesBO.QueryForList(new BetterSite.Domain.M_Types() { TypeCode = "DR" }).Cast<M_Types>().ToList();
                if (types.Count > 0)
                {
                    typeId = types[0].TypeId;
                }
                else {
                    typesBO.Insert(new BetterSite.Domain.M_Types() { TypeCode = "DR", TypeName = "导入" });
                    typeId = typesBO.QueryForEntityList(new BetterSite.Domain.M_Types() { TypeCode = "DR" })[0].TypeId;
                }
               
                foreach (var item in sites)
                {
                    item.TypeId = typeId;
                    if ((int)sitesBO.QueryForObject(item) == 0)
                    {
                        sitesBO.Insert(item);
                        successCount++;
                    }
                }
                json.Data = new
                   {
                       success = true,
                       msg = "共" + importCount + "条记录，导入成功" + successCount + "条记录"
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
