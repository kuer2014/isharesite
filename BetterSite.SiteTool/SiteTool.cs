using BetterSite.BusinessObject;
using BetterSite.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace BetterSite.SiteTool
{
    public partial class SiteTool : Form
    {

        public SiteTool()
        {

            InitializeComponent();

        }

        private void SiteTool_Load(object sender, EventArgs e)
        {
            ////这里构造一个List，当然也可以从数据库中获取
            List<M_Types> typeList = new List<M_Types>() {
                   new M_Types { TypeName="待审核",TypeId="712A8505-D927-49DD-9B4E-1F276EDE6746"},
                 new M_Types { TypeName="找资源",TypeId="7670B2EA-5E3C-4072-B02E-577D893AA7F9"},
                  new M_Types { TypeName="学技术",TypeId="984E08AE-CADE-4522-A674-7EFEDC056B91"},
                new M_Types { TypeName="善发现",TypeId="50822E1E-C5E2-4B3C-B023-0B857BA40E18"},             
                   new M_Types { TypeName="爱生活",TypeId="914DD8D4-9934-4CDD-8DC3-E07C0CE87BF6"},
            };
            TypeId.DataSource = typeList;//绑定
            TypeId.DisplayMember = "TypeName";//显示的文本
            TypeId.ValueMember = "TypeId";//对应的值
            TypeId.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void PullData_Click(object sender, EventArgs e)
        {
            BackWorker.RunWorkerAsync(2000/*参数是可选的*/);
        }
        /// <summary>
        /// 拉取数据
        /// </summary>
        private void _PullData()
        {
            string url = SourceUrl.Text;
            string appExePath = System.Windows.Forms.Application.StartupPath;//(.exe文件所在的目录)// System.Windows.Forms.Application.ExecutablePath;//(.exe文件所在的目录+.exe文件名)
            string appExeDrive = appExePath.Substring(0, 2);
            string cmd = $"{appExeDrive }&cd {appExePath}\\Phantomjs\\&phantomjs getsitedata.js " + url;
            // string cmd = @"F:&cd F:\NavSite\phantom\0421\&phantomjs getsitedata.js "+ url;
            string sitedataPath = $"{appExePath}\\Phantomjs\\tempdata\\sitedata.txt";
            //@"F:\NavSite\phantom\0421\data\sitedata.txt"
            string result = string.Empty;
            PhantomjsHelper.RunCmd(cmd, out result);
            if (!string.IsNullOrEmpty(result))
            {
                string[] siteSourceData = File.ReadAllLines(sitedataPath);
                if (siteSourceData != null)
                {
                    int cnt = siteSourceData.Count();
                    if (cnt > 0)
                        SiteCode.Text = siteSourceData[0];
                    if (cnt > 1)
                        SiteName.Text = siteSourceData[1];
                    if (cnt > 2)
                        SiteUrl.Text = siteSourceData[2];
                    if (cnt > 3)
                        SiteProfile.Text = siteSourceData[3];
                    if (cnt > 4)
                        SiteKeywords.Text = siteSourceData[4];
                    if (cnt > 5)
                    {
                        SiteImgBase64.Text = siteSourceData[5];
                        // Image img = Image.FromFile(@"F:\NavSite\phantom\0421\data\siteimg.jpg");
                        // Image img = Image.FromStream(System.Net.WebRequest.Create(imgurl).GetResponse().GetResponseStream());                        
                        SiteImg.Image = ImageHelper.ToImage(siteSourceData[5]);
                    }
                }
            }
        }
        /// <summary>
        /// 推送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PushData_Click(object sender, EventArgs e)
        {
            M_Sites m = new M_Sites();
            m.SiteCode = SiteCode.Text;
            m.SiteName = SiteName.Text;
            m.SiteUrl = SiteUrl.Text;
            m.SiteProfile = SiteProfile.Text;
            m.TypeId = TypeId.SelectedValue.ToString();
            m.SiteOrderNumber = int.Parse(SiteOrderNumber.Text);
            m.SiteImgBase64 = SiteImgBase64.Text.Replace('+', '-').Replace('/', '_');

            // string result = RequestHelper.PostHttp("http://localhost:8080/siteapi/Add/", "token=2CBa31gg4s7dB&entityJson=" + JsonConvert.SerializeObject(m));
            string result = RequestHelper.PostHttp("http://www.isharesite.com/siteapi/Add/", "token=2CBa31gg4s7dB&entityJson=" + JsonConvert.SerializeObject(m));

            SystemMsg.Text = "推送数据－" + result;//JsonConvert.SerializeObject(m);//
            if (result.Equals("添加成功"))
                SourceUrl.Text = "";
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyMsg_Click(object sender, EventArgs e)
        {

            if (SystemMsg.Text != "")
                Clipboard.SetDataObject(SystemMsg.Text);
        }
        ////粘贴： 
        //private void button2_Click(object sender, System.EventArgs e)
        //{
        //    IDataObject iData = Clipboard.GetDataObject();
        //    if (iData.GetDataPresent(DataFormats.Text))
        //    {
        //        textBox2.Text = (String)iData.GetData(DataFormats.Text);
        //    }
        //}
        private void ClearMsg_Click(object sender, EventArgs e)
        {
            SystemMsg.Text = string.Empty;
        }

        private void BackWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            SystemMsg.Text = "拉取数据－进行中..";
            //for (int i = 1; i < 11; i++)
            //{
            //    System.Threading.Thread.Sleep(2000);
            //    BackWorker.ReportProgress(i * 10);
            //    if (BackWorker.CancellationPending)
            //    {
            //        e.Cancel = true;
            //        return;
            //    }
            //}
            _PullData();

            // BackWorker.ReportProgress("进行中..");
        }

        private void BackWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SystemMsg.Text = "拉取数据－完成。";
        }
    }
}
