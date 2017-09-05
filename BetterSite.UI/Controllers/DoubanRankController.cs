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
        string title = System.Configuration.ConfigurationManager.AppSettings["title"];
        public ActionResult Index(string desc)
        {
            desc=(desc+"").ToLower();
            M_DoubanRank where = new M_DoubanRank();
            where.Status = 1;
            switch (desc) {
                case "豆瓣评分最高的图书排行榜top100":
                    where.Category = 1;where.Type = 1;
                    break;
                case "豆瓣评价人数最多的图书排行榜top100":
                    where.Category = 1; where.Type = 2;
                    break;
                case "豆瓣评分最高的电影排行榜top100":
                    where.Category = 2; where.Type = 1;
                    break;
                case "豆瓣评价人数最多的电影排行榜top100":
                    where.Category = 2; where.Type = 2;
                    break;
                case "豆瓣评分最高的音乐排行榜top100":
                    where.Category = 3; where.Type = 1;
                    break;
                case "豆瓣评价人数最多的音乐排行榜top100":
                    where.Category = 3; where.Type = 2;
                    break;
                default:
                    break;
            }
            ViewBag.Title = desc+ "(每周更新) - " + title;
            //ViewBag.Title = "文章 - " + title;
            ViewBag.Keywords = desc;
            ViewBag.Description = desc + ",每周更新.";
            var list = rankBO.QueryForList(where).Cast<M_DoubanRank>().ToList();
            if (where.Type == 2)
            {
                //人数多
                list = list.OrderByDescending(o1 => o1.RatingNum).OrderByDescending(o2 => o2.RatingPeople).ToList();

            }
            else {
                //评分高
                list = list.OrderByDescending(o2 => o2.RatingPeople).OrderByDescending(o1 => o1.RatingNum).ToList();
              
            }
            //var list = rankBO.QueryForEntityList(where);
            return View(list);
        }

      
    }
}
