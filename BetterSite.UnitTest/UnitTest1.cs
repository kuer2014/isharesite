using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using BetterSite.UI.Utility;
using BetterSite.Domain;
using BetterSite.BusinessObject;
using System.Linq;
using System.IO;

namespace BetterSite.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// 生成网站缩略图
        /// </summary>
        [TestMethod]
        public void BuildImage()
        {
            SitesBO sitesBO = new SitesBO();
            M_Sites where = new M_Sites();
            where.SiteIsActive = true;
            where.TypeCode = "ZX";
            //where.SearchText = "鸠摩电子书搜索引擎";
            var list = sitesBO.QueryForList(where).Cast<M_Sites>();
            OLayer ow = new OLayer();
            foreach (var site in list)
            {
                //ow.CaptureImage(site.SiteUrl, "D:\\cap\\" + site.SiteCode + ".jpg");
            }
        }
        /// <summary>
        /// 百度推送  curl推送
        /// </summary>
        [TestMethod]
        public void CurlTest() {
            string url = "http://data.zz.baidu.com/urls?site=www.isharesite.com&token=hzM0UEGaAryxFNUx";
            string param = File.ReadAllText("D:\\urls\\urls.txt");// "http://www.isharesite.com/Sites/SITE1451619620817\nhttp://www.isharesite.com/Sites/SITE1489735778357" ;
          
            string msg = "";
               msg = RequestHelper.PostData(url,param);
        }
        [TestMethod]
        public void GetGUID() {
            string[] guids = { Guid.NewGuid().ToString().ToUpper(), Guid.NewGuid().ToString().ToUpper(), Guid.NewGuid().ToString().ToUpper(), Guid.NewGuid().ToString().ToUpper() };
        }
      
    }
}

