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
        static void Main(string[] args)
        {

            string cmd = @"F:&cd F:\NavSite\phantom\0421\&phantomjs getsitedata.js https://www.baidu.com/";
          //  string cmd = @"F:\NavSite\phantom\0421>phantomjs getsitedata.js https://www.baidu.com/";
            string output = "";
           CmdHelper.RunCmd(cmd, out output);
            Console.WriteLine(output);
            Console.ReadKey();
        }     

        //[STAThread]
        //static void Main(string[] args)
        //{
        //    SitesBO sitesBO = new SitesBO();
        //    M_Sites where = new M_Sites();
        //    OLayer ow = new OLayer();
        //    //Console.ReadKey();
        //    Console.WriteLine("按类型/1，按url/2,请输入数字:");
        //    string cate = Console.ReadLine();
        //    if (cate == "1")
        //    {
        //        Console.WriteLine("请输入类型－两位大写字母;全部请输入ALL");
        //        string type = Console.ReadLine();

        //        while (string.IsNullOrWhiteSpace(type))
        //        {
        //            Console.WriteLine("请输入类型－两位大写字母;全部请输入ALL");
        //            type = Console.ReadLine();
        //        };
        //        if (type.ToUpper() == "ALL") type = "";
        //        where.SiteIsActive = true;
        //        where.TypeCode = type;
        //        var list = sitesBO.QueryForList(where).Cast<M_Sites>();
        //        string result = string.Empty;
        //        int count = list.Count();
        //        int curNum = 0;
        //        foreach (var site in list)
        //        {
        //            curNum += 1;
        //            result = ow.CaptureImage(site.SiteUrl, "D:\\cap\\" + site.SiteCode + ".jpg");
        //            Console.WriteLine($"{site.SiteCode}.jpg －{curNum}/{count}:" + result);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("请输入要生成图片的网站URL:");
        //        string url = Console.ReadLine();
        //        Console.WriteLine("请输入要生成图片的网站SiteCode:");
        //        string code = Console.ReadLine();
        //        ow.CaptureImage(url, "D:\\cap\\" + code + ".jpg");
        //        Console.WriteLine($"{code}.jpg成功");
        //    }
        //    Console.WriteLine("完成,按任意键关闭.");
        //    Console.ReadKey();
        //}
    }
}
