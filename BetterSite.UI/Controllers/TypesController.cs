using BetterSite.BusinessObject;
using BetterSite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Controllers
{
    public class TypesController : Controller
    {
        private readonly TypesBO typesBO = new TypesBO();
        //
        // GET: /Admin/Types/

        public ActionResult Index()
        {
            IList<M_Types> types = typesBO.QueryForEntityList(null);
            return View(types);
        }
        public ActionResult _TypeCrtl()
        {
            IList<M_Types> types = typesBO.QueryForEntityList(null);
            return PartialView("_TypeCrtl",types);
        }     
    }
}
