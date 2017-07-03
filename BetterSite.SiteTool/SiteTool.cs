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
            //   backgroundWorker1.WorkerReportsProgress = true;
            BackWorker.WorkerSupportsCancellation = true;

        }
        
        private void SiteTool_Load(object sender, EventArgs e)
        {
            ////这里构造一个List，当然也可以从数据库中获取
            List<M_Types> typeList = new List<M_Types>() {                 
                 new M_Types { TypeName="个人提升",TypeId="BE4D99A2-0BA8-4578-AEA5-09AD5BDED64A"},
                  new M_Types { TypeName="在线工具",TypeId="50822E1E-C5E2-4B3C-B023-0B857BA40E18"},
                new M_Types { TypeName="免费素材",TypeId="7670B2EA-5E3C-4072-B02E-577D893AA7F9"},             
                   new M_Types { TypeName="发现好玩",TypeId="984E08AE-CADE-4522-A674-7EFEDC056B91"},
                     new M_Types { TypeName="行业专栏",TypeId="1C942B35-A249-4A7B-90FF-982457C69793"},
                        new M_Types { TypeName="便民查询",TypeId="914DD8D4-9934-4CDD-8DC3-E07C0CE87BF6"},
                         new M_Types { TypeName="其它",TypeId="712A8505-D927-49DD-9B4E-1F276EDE6746"},
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
        private void CancelPullData_Click(object sender, EventArgs e)
        {
            if (BackWorker.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                BackWorker.CancelAsync();
            }
        }
        /// <summary>
        /// 通过URL拉取数据保存为本地文本文件
        /// </summary>
        private string _PullData(string url)
        {
             string sitedataPath = string.Empty;
            //string url = SourceUrl.Text;
            try
            {
                string appExePath = System.Windows.Forms.Application.StartupPath;//(.exe文件所在的目录)// System.Windows.Forms.Application.ExecutablePath;//(.exe文件所在的目录+.exe文件名)
                string appExeDrive = appExePath.Substring(0, 2);
                string sitecode = "SITE" + Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds);
                string cmd = $"{appExeDrive }&cd {appExePath}\\Phantomjs\\&phantomjs getsitedata.js " + url + " " + sitecode;
                // string cmd = @"F:&cd F:\NavSite\phantom\0421\&phantomjs getsitedata.js "+ url;  
                // System.Threading.Timer Thread_Time = new System.Threading.Timer(Thread_Timer_Method, null, 0, 20);
              //  System.Threading.Thread t = new System.Threading.Thread(Thread_Method);
                //t.Start();
                string result = string.Empty;
                PhantomjsHelper.RunCmd(cmd, out result);
                if (!string.IsNullOrEmpty(result))
                {
                    sitedataPath = $"{appExePath}\\Phantomjs\\tempdata\\sitedata_" + sitecode + ".txt";
                    //@"F:\NavSite\phantom\0421\data\sitedata.txt"
                }
            }
            catch {

            }
            return sitedataPath;
        }
        void Thread_Method(object o)
        {            
            System.Threading.Thread.Sleep(3000);
            throw new Exception();
        }
        /// <summary>
        /// 读取本地sitedata文本文件填充表单
        /// </summary>
        /// <param name="sitedataPath"></param>
        private void FillForm(string sitedataPath) {
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
                    SiteImg.Image = ImageHelper.ToImage(SiteImgBase64.Text);//siteSourceData[5]
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
            if (string.IsNullOrWhiteSpace(SiteCode.Text) || string.IsNullOrWhiteSpace(SiteName.Text) || string.IsNullOrWhiteSpace(SiteUrl.Text) || string.IsNullOrWhiteSpace(SiteProfile.Text))
            { MessageBox.Show("请装载数据后重试!"); return; }
            if (SiteProfile.Text.Length < 10) {
                MessageBox.Show("网站描述需在10个字符以上!"); return;
            }
            M_Sites m = new M_Sites();
            m.SiteCode = SiteCode.Text;
            m.SiteName = SiteName.Text;
            m.SiteUrl = SiteUrl.Text;
            m.SiteProfile = SiteProfile.Text;// System.Web.HttpUtility.UrlEncode( SiteProfile.Text);

            m.TypeId = TypeId.SelectedValue.ToString();
            m.SiteOrderNumber = int.Parse(SiteOrderNumber.Text);
            m.SiteImgBase64 = SiteImgBase64.Text.Replace('+', '-').Replace('/', '_');
            string sitetags = GetSiteTags();
            if (string.IsNullOrWhiteSpace(sitetags)) {
                MessageBox.Show("请为网站选择标签!"); return;
            }
            // string result = RequestHelper.PostHttp("http://localhost:8080/siteapi/Add/", "token=2CBa31gg4s7dB&entityJson=" + JsonConvert.SerializeObject(m));
            string result = RequestHelper.PostHttp("http://www.isharesite.com/siteapi/Add/", "token=2CBa31gg4s7dB&sitetags="+ sitetags + "&entityJson=" + JsonConvert.SerializeObject(m));
           // string result = RequestHelper.PostHttp("http://localhost:8080/siteapi/Add/", "token=2CBa31gg4s7dB&sitetags=" + sitetags + "&entityJson=" + JsonConvert.SerializeObject(m));
            //var newSitefile=sitefile.Where(s=>s.SiteUrl != SiteUrl.Text ).ToList();
           // sitefile = newSitefile;
            sitefile.Remove(sitefile.Where(s => s.SiteUrl == SiteUrl.Text).FirstOrDefault());
            UpdateCmbviewsite(sitefile);
            SystemMsg.Text = result + ":推送数据－成功添加一个网站。| 完成时间:" + DateTime.Now;
            if (result.Equals("添加成功"))
                SourceUrl.Text = "";
        }
        private string GetSiteTags() {
            string tagsStr = string.Empty;
            foreach (Control item in this.Controls)
            {                
                if (item is CheckBox)
                {
                    var chk = (CheckBox)item;
                    if (chk.Checked == true) {
                        tagsStr +=  chk.Tag+",";
                    }
                   
                }
            }  
            return tagsStr;
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
            BuildData();
        }
        private List<SiteFile> sitefile = new List<SiteFile>();
        private void BuildData() {
            string urls = SourceUrl.Text;
            if (string.IsNullOrWhiteSpace(urls))
            {
                SystemMsg.Text = "未输入网站URL。";
                return;
            }
            SystemMsg.Text = "拉取数据－开始.. | 开始时间:" + DateTime.Now;
            string[] urlarr = urls.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //Dictionary<string, string> sitefile = new Dictionary<string, string>();
            // List<dynamic> sitefile = new List<dynamic>();
            string sitedataPath = string.Empty;
            for (int i = 0; i < urlarr.Count(); i++)
            {
               // System.Threading.Thread.Sleep(100);
                sitedataPath = _PullData(urlarr[i]);
                if (string.IsNullOrEmpty(sitedataPath)) {
                    SystemMsg.Text = $"拉取数据－最新失败:{i + 1}/{urlarr.Count()} [{ urlarr[i]}].";
                } else { 
                sitefile.Add(new SiteFile { SiteUrl = urlarr[i], SiteFileName = sitedataPath });
                    SystemMsg.Text = $"拉取数据－最新成功:{i + 1}/{urlarr.Count()} [{ urlarr[i]}].";
                }
                
            }
            //CmbViewSite.DataSource = sitefile;//绑定
            //CmbViewSite.DisplayMember = "SiteUrl";//显示的文本
            //CmbViewSite.ValueMember = "fileName";//对应的值
            //CmbViewSite.DropDownStyle = ComboBoxStyle.DropDownList;
         
        }
        private void BackWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {            
            SystemMsg.Text = $"拉取数据－完成。共计{sitefile.Count}条。| 完成时间:" + DateTime.Now;
            UpdateCmbviewsite(sitefile);
            SourceUrl.Text = "";
            MessageBox.Show("OK");
        }
        private void UpdateCmbviewsite(IList<SiteFile> sitefile) {
            CmbViewSite.DataSource = null;
            CmbViewSite.DataSource = sitefile;//绑定
            CmbViewSite.DisplayMember = "SiteUrl";//显示的文本
            CmbViewSite.ValueMember = "SiteFileName";//对应的值
            CmbViewSite.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void SiteImgBase64_TextChanged(object sender, EventArgs e)
        {
            SiteImg.Image = ImageHelper.ToImage(SiteImgBase64.Text);
        }

        private void TypeId_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }
        private Control.ControlCollection RemoveTag(Control.ControlCollection cc) {
            bool flag = false;
            foreach (Control item in this.Controls)
            {
                // if (item.GetType().ToString() == "System.Windows.Forms.CheckBox")
                if (item is CheckBox)
                {
                    flag = true;
                    this.Controls.Remove(item);
                }
            }
            if (flag)
                return RemoveTag(this.Controls);
            else
                return null;
        }
        /// <summary>
        /// 构建标签
        /// </summary>
        private void BulidTagChk()
        {

            ////清除之前标签
            RemoveTag(this.Controls);
            //foreach (Control item in this.Controls)
            //{
            //   // if (item.GetType().ToString() == "System.Windows.Forms.CheckBox")
            //        if (item is CheckBox)
            //    {
            //        this.Controls.Remove(item);
            //    }
            //}
            //for (int i = 0; i < this.pnl.Controls.Count; i++)
            //{

            //        this.Controls.RemoveByKey(string.Format("chk{0}", i.ToString()));

            //}
            var url = "http://www.isharesite.com/siteapi/GetTags?token=2CBa31gg4s7dB";
              // var url = "http://localhost:8080/siteapi/GetTags?token=2CBa31gg4s7dB";
            string result = RequestHelper.PostHttp(url, "typeId=" + TypeId.SelectedValue.ToString());// "7670B2EA-5E3C-4072-B02E-577D893AA7F9");
          
            if (!string.IsNullOrWhiteSpace(result)) { 
            var taglist = JsonConvert.DeserializeObject<IList<M_Tags>>(result).OrderBy(t=>t.TagName).ToList();
                int x=0, y=380,ii=0;
                for (int i = 0; i < taglist.Count; i++)
                {
                    System.Windows.Forms.CheckBox chkb = new System.Windows.Forms.CheckBox();               

                    x = ii *80;
                    ++ii;
                    //if (taglist[i].TagName.Length >=3) x += 10;
                    //if (taglist[i].TagName.Length >=5) x += 10;
                    if (ii % 5 == 0) { y += 20; ii = 0; }
                    chkb.Location = new Point(x,y);
                    chkb.AutoSize = true;
                    chkb.Name = string.Format("chk{0}", i.ToString());
                    //chkb.Size = new System.Drawing.Size(78, 16);
                  //  chkb.TabIndex = 11;
                    chkb.Text = taglist[i].TagName;
                    chkb.UseVisualStyleBackColor = true;
                    chkb.Tag = taglist[i].TagId;
                    
                    this.Controls.Add(chkb);
                   // chkb.BringToFront();
                  //  chkb.Visible = true;

                }
               // this.pnl.BringToFront();
               // this.pnl.Visible = true;
            }
        }

        private void TypeId_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BulidTagChk();
        }

        private void BtnViewData_Click(object sender, EventArgs e)
        {
            string sitedataPath=CmbViewSite.SelectedValue.ToString();
            FillForm(sitedataPath);
        }

        private void SaveImg_Click(object sender, EventArgs e)
        {
            DialogResult dr = folderBrowserDialog1.ShowDialog();// 是调用文件浏览器控件；      
            if (dr == System.Windows.Forms.DialogResult.OK) // 是判断文件浏览器控件是否返回ok，即用户是否确定选择。如果确定选择，则弹出用户在文件浏览器中选择的路径：   
            {
                //    MessageBox.Show(folderBrowserDialog1.SelectedPath);
                // SiteImg.Image.Save(folderBrowserDialog1.SelectedPath + "\\" + SiteCode.Text + ".jpg");//,System.Drawing.Imaging.ImageFormat.Jpeg);

                string filename = ImageHelper.Base64StringToFile(SiteImgBase64.Text, folderBrowserDialog1.SelectedPath, SiteCode.Text);
                if (filename != string.Empty) {
                    MessageBox.Show("保存成功");
                }
            }
        }
    }
}
