﻿using BetterSite.BusinessObject;
using BetterSite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Areas.v1.Controllers
{
    public class TagsController : Controller
    {
        private readonly TagsBO tagsBO = new TagsBO();
        private readonly TypesBO typesBO = new TypesBO();
        //
        // GET: /Admin/Types/

        public ActionResult Index()
        {
            IList<M_Tags> tags = tagsBO.QueryForList(null).Cast<M_Tags>().ToList(); 
            return View(tags);
        }
        /// <summary>
        /// 【标签控件】根据TypeCode查找标签
        /// </summary>
        /// <param name="TypeCode">类型编号</param>
        /// <param name="TagId">状态（页面已勾选数据）</param>
        /// <returns>标签数据</returns>
            public ActionResult _TagCrtl(string TypeCode, string[] Tag)
        {
            var typeId = string.Empty;
            IList<M_Tags> tags = new List<M_Tags>();
            if (!string.IsNullOrWhiteSpace(TypeCode))
            {
                IList<M_Types> types = typesBO.QueryForEntityList(new BetterSite.Domain.M_Types() { TypeCode = TypeCode });
                if (types != null && types.Count() > 0)
                {
                    typeId = types[0].TypeId;
                    tags = tagsBO.QueryForEntityListByTypeId(typeId);
                }

            }
            else {
                tags = tagsBO.QueryForEntityListByTypeId("");
            }
           // ViewBag.ZY=tags.Where(t=>t.TagCode== "ZY")
           // tags = tags.Take(15).ToList();
            //IList<M_Tags> tags = tagsBO.QueryForList(null).Cast<M_Tags>().ToList();
            if (Tag != null && Tag.Count() > 0) ViewBag.CheckdTags = Tag.ToList();
            return PartialView("_TagCrtl", tags);
        }     
    }
}
