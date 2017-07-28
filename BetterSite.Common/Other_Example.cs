using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace BetterSite.Common
{
    class Other_Example
    {

        /// <summary>
        /// 读取配置文件
        /// 接收变量静态时，为数据安全，请加readonly修饰，
        /// 注意引用System.Configuration 程序集
        /// </summary>
        static readonly string AppDownUrl = System.Configuration.ConfigurationManager.AppSettings["AppDownUrl"];
        decimal total = 0.00M; //定义decimal变量并初始值                                                                                            

        /// <summary>
        /// 流
        /// </summary>
        void streamFunc()
        {
            Stream filesstream = new FileStream("path", FileMode.Open);//文件到流
            StreamReader reader = new StreamReader(filesstream);
            string filestr = reader.ReadToEnd();  ///Stram流 至字符串
            filesstream.Close();
            reader.Close();

        }
        void xmlFunc()
        {
            string xml = "content";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml); //字符串到XML对象
            XmlNode xn = doc.SelectSingleNode("dict");//取指定节点  
            XmlNodeList xnl = xn.ChildNodes;    // 得到节点的所有子节点
            string xnltxt = xnl[0].InnerText;//取某节点的内容
        }
        void fileFunc()
        {
            string descTemplateStr = File.ReadAllText("path", System.Text.Encoding.UTF8);//读取文件到字符串
            File.WriteAllText("path2", descTemplateStr, System.Text.Encoding.UTF8);//写字符串到文件
        }
        void datetimeFunc()
        {
            //1 计算与客户端时间差
            long clientTimeStamp = 1489992926300;//客户端的时间
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); //1970年1月1号在本地时区的时间
            DateTime cNowTime = startTime.AddMilliseconds(clientTimeStamp);//计算客户端的时间
            DateTime sNowTime = DateTime.Now;
            TimeSpan dif = cNowTime - sNowTime; //计算差

            //2 toString()
            DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
            //字符串到时间
            DateTime resTime;
            DateTime.TryParse("2017-06-26 10:50:11", out resTime);
        }
        void jsonFunc() {
            ///json字符串到对象
           // var equipment = JsonConvert.DeserializeObject<EquipmentEntity>(paramstr);
        }
        /// <summary>
        /// 验证苹果手机 IDFA格式是否正确
        /// </summary>
        /// <param name="idfa"></param>
        /// <returns></returns>
        public static bool TestIDFA(string idfa)
        {
            if (idfa == "00000000-0000-0000-0000-000000000000") return false;
            //  IDFA: CCD6E1CD - 8C4B - 40CB - 8A62 - 4BBC7AFE07D6
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$");
            return r.IsMatch(idfa);
            //MatchCollection mc = r.Matches(idfa);
            //return mc != null;
            //string reg = @"〔\w{1}〕";
            //Regex.Matches(files, reg)            
        }
        public static void stringfunc()
        {
            "a".PadLeft(3, '0');  //不够3位补0
        }
        /// <summary>
        /// 去掉括号和括号内的内容，只去首次出现的
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string SubStr(string txt)
        {
            //string txt = "<p>（本报美里26日讯）拿督）格拉（瓦表示﹐为了）应付多年来的交通";
            txt = txt.Replace("（", "(").Replace("）", ")");
            int n = txt.IndexOf("(");
            int m = txt.IndexOf(")");
            if (n > 0 && m > n)
            {
                string _txt = txt.Substring(n, m - n + 1);
                txt = txt.Replace(_txt, "");  //string ntxt = "<p>拿督）格拉（瓦表示﹐为了）应付多年来的交通";
            }
            return txt;
        }

    }
}
