using BetterSite.BusinessObject;
using BetterSite.Domain;
using BetterSite.UI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSite.CMD
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Console.ReadKey();
            Console.WriteLine("请输入类型－两位大写字母;全部请输入ALL");
            string type= Console.ReadLine();
           
            while (string.IsNullOrWhiteSpace(type)) {
                Console.WriteLine("请输入类型－两位大写字母;全部请输入ALL");
                 type = Console.ReadLine();
            } ;
            if (type.ToUpper() == "ALL") type = "";
            SitesBO sitesBO = new SitesBO();
            M_Sites where = new M_Sites();
            where.SiteIsActive = true;
           
            where.TypeCode = type;
            var list = sitesBO.QueryForList(where).Cast<M_Sites>();
            OLayer ow = new OLayer();
            string result = string.Empty;
            int count = list.Count();
            int curNum = 0;
            foreach (var site in list)
            {
                curNum += 1;
                result= ow.CaptureImage(site.SiteUrl, "D:\\cap\\" + site.SiteCode + ".jpg");
                Console.WriteLine($"{site.SiteCode}.jpg －{curNum}/{count}:"+result);
            }
            Console.WriteLine("完成,按任意键关闭.");
            Console.ReadKey();
        }
    }
}
