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
            //计算与客户端时间差
            long clientTimeStamp = 1489992926300;//客户端的时间
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); //1970年1月1号在本地时区的时间
            DateTime cNowTime = startTime.AddMilliseconds(clientTimeStamp);//计算客户端的时间
            DateTime sNowTime = DateTime.Now;
            TimeSpan dif = cNowTime - sNowTime; //计算差
        }
        void jsonFunc() {
            ///json字符串到对象
           // var equipment = JsonConvert.DeserializeObject<EquipmentEntity>(paramstr);
        }

    }
}
