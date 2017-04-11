using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using BetterSite.UI.Utility;
using BetterSite.Domain;
using BetterSite.BusinessObject;
using System.Linq;

namespace BetterSite.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SitesBO sitesBO = new SitesBO();
            M_Sites where = new M_Sites();
            where.SiteIsActive = true;
            where.TypeCode = "ZX";
            var list = sitesBO.QueryForList(where).Cast<M_Sites>();
            OLayer ow = new OLayer();
            foreach (var site in list)
            {
                ow.CaptureImage(site.SiteUrl, "D:\\cap\\" + site.SiteCode + ".jpg");
            }
        }
    }
}

