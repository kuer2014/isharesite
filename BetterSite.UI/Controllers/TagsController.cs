using BetterSite.BusinessObject;
using BetterSite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Controllers
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
        public ActionResult _TagCrtl(string TypeCode,string[] TagId)
        {
            //ViewBag.Typecode = TypeCode;
#region 根据TypeCode查找标签
            var typeId = string.Empty;
            if (!string.IsNullOrWhiteSpace(TypeCode)) {
            IList<M_Types> types = typesBO.QueryForEntityList(new BetterSite.Domain.M_Types() { TypeCode = TypeCode });
             typeId = types[0].TypeId;
            }
            IList<M_Tags> tags= tagsBO.QueryForEntityListByTypeId(typeId);
#endregion
            //IList<M_Tags> tags = tagsBO.QueryForList(null).Cast<M_Tags>().ToList();
            if(TagId!=null&& TagId.Count()>0) ViewBag.CheckdTags = TagId.ToList();
            return PartialView("_TagCrtl",tags);
        }     
    }
}
