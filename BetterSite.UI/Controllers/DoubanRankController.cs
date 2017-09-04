using BetterSite.BusinessObject;
using BetterSite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterSite.UI.Controllers
{
    public class DoubanRankController : Controller
    {
        //
        // GET: /DoubanRank/
        private readonly DoubanRankBO rankBO = new DoubanRankBO();
        public ActionResult Index(string desc)
        {
            M_DoubanRank where = new M_DoubanRank();
            where.Status = 1;
            switch (desc) {
                case "豆瓣评分最高的图书排行榜TOP100":
                    where.Category = 1;where.Type = 1;
                    break;
                case "豆瓣评价人数最多的图书排行榜TOP100":
                    where.Category = 1; where.Type = 2;
                    break;
                case "豆瓣评分最高的电影排行榜TOP100":
                    where.Category = 2; where.Type = 1;
                    break;
                case "豆瓣评价人数最多的电影排行榜TOP100":
                    where.Category = 2; where.Type = 2;
                    break;
                case "豆瓣评分最高的音乐排行榜TOP100":
                    where.Category = 3; where.Type = 1;
                    break;
                case "豆瓣评价人数最多的音乐排行榜TOP100":
                    where.Category = 3; where.Type = 2;
                    break;
                default:
                    break;
            }
            ViewBag.Title = desc+ "(每周更新)";
            var list = rankBO.QueryForList(where).Cast<M_DoubanRank>().ToList();
            if (where.Type == 2)
            {
                list = list.OrderBy(o1 => o1.RatingNum).OrderByDescending(o2 => o2.RatingPeople).ToList();
            }
            else {
                list = list.OrderByDescending(o2 => o2.RatingPeople).OrderBy(o1 => o1.RatingNum).ToList();
            }
            //var list = rankBO.QueryForEntityList(where);
            return View(list);
        }

      
    }
}
