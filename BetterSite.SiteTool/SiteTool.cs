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
                new M_Types { TypeName="善发现",TypeId="50822E1E-C5E2-4B3C-B023-0B857BA40E18"},
                new M_Types { TypeName="待审核",TypeId="712A8505-D927-49DD-9B4E-1F276EDE6746"},
                 new M_Types { TypeName="找资源",TypeId="7670B2EA-5E3C-4072-B02E-577D893AA7F9"},
                  new M_Types { TypeName="学技术",TypeId="984E08AE-CADE-4522-A674-7EFEDC056B91"},
                   new M_Types { TypeName="爱生活",TypeId="914DD8D4-9934-4CDD-8DC3-E07C0CE87BF6"},
            };
            TypeId.DataSource = typeList;//绑定
            TypeId.DisplayMember = "TypeName";//显示的文本
            TypeId.ValueMember = "TypeId";//对应的值
            TypeId.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void PullData_Click(object sender, EventArgs e)
        {
            string url = SourceUrl.Text;
            string cmd = @"F:&cd F:\NavSite\phantom\0421\&phantomjs getsitedata.js "+ url;
            string result =string.Empty;
            PhantomjsHelper.RunCmd(cmd, out result);
            if (!string.IsNullOrEmpty(result)) {
                string[] siteSourceData = File.ReadAllLines(@"F:\NavSite\phantom\0421\data\sitedata.txt");
                if (siteSourceData != null) {
                    int cnt = siteSourceData.Count();
                    if (cnt> 0)
                    SiteCode.Text = siteSourceData[0];
                    if (cnt > 1)
                        SiteName.Text = siteSourceData[1];
                    if (cnt > 2)
                        SiteUrl.Text = siteSourceData[2];
                    if (cnt > 3)
                        SiteProfile.Text = siteSourceData[3];
                    if (cnt > 4) {
                        SiteImgBase64.Text = siteSourceData[4];
                       // Image img = Image.FromFile(@"F:\NavSite\phantom\0421\data\siteimg.jpg");
                        // Image img = Image.FromStream(System.Net.WebRequest.Create(imgurl).GetResponse().GetResponseStream());
                        
                        SiteImg.Image =ImageHelper.ToImage(siteSourceData[4]);
                    }
                }
            }
        }

        private void PushData_Click(object sender, EventArgs e)
        {
            M_Sites m = new M_Sites();
            m.SiteCode = SiteCode.Text;
            m.SiteName = SiteName.Text;
            m.SiteUrl = SiteUrl.Text;
            m.SiteProfile = SiteProfile.Text;
            m.TypeId = TypeId.SelectedValue.ToString();
            m.SiteOrderNumber = int.Parse(SiteOrderNumber.Text);
            m.SiteImgBase64 = SiteImgBase64.Text;
            string result = RequestHelper.PostHttp("http://localhost:8080/SiteHandler.ashx", "token=2CBa31gg4s7dB&imgbase64="+m.SiteImgBase64+"&entityJson=" + JsonConvert.SerializeObject(m));
           //string result = RequestHelper.PostHttp("http://localhost:8080/siteapi/Add/", "token=2CBa31gg4s7dB&entityJson=" + JsonConvert.SerializeObject(m));
            // string result = RequestHelper.PostHttp("http://www.isharesite.com/siteapi/Add/?token=abc", "entityJson=" + JsonConvert.SerializeObject(m));

            SystemMsg.Text = JsonConvert.SerializeObject(m);// result;
        }
        private void CopyMsg_Click(object sender, EventArgs e)
        {
            
            if (SystemMsg.Text != "")
                Clipboard.SetDataObject(SystemMsg.Text);
        }
        private void ClearMsg_Click(object sender, EventArgs e)
        {
            SystemMsg.Text = string.Empty;
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
    }
}
