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
using System.Windows.Forms;

namespace BetterSite.SiteTool
{
    public partial class Capture : Form
    {
        public Capture()
        {
            InitializeComponent();
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync(2000/*参数是可选的*/);
        
        }

        /// <summary>
        /// 通过URL拉取数据保存为本地文本文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="count">条数</param>
        /// <returns></returns>
        private string _PullData(string url,int count)
        {
            string sitedataPath = string.Empty;
            string result = string.Empty;
            //string url = SourceUrl.Text;
            try
            {
                string appExePath = System.Windows.Forms.Application.StartupPath;//(.exe文件所在的目录)// System.Windows.Forms.Application.ExecutablePath;//(.exe文件所在的目录+.exe文件名)
                string appExeDrive = appExePath.Substring(0, 2);
                //string sitecode = "SITE" + Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds);
                string cmd = $"{appExeDrive }&cd {appExePath}\\Python\\&python seehua.py " + url + " " + count;
                // string cmd = @"F:&cd F:\NavSite\phantom\0421\&phantomjs getsitedata.js "+ url;  
                // System.Threading.Timer Thread_Time = new System.Threading.Timer(Thread_Timer_Method, null, 0, 20);
                //  System.Threading.Thread t = new System.Threading.Thread(Thread_Method);
                //t.Start();              
                PhantomjsHelper.RunCmd(cmd, out result);
                if (!string.IsNullOrEmpty(result))
                {
                    
                    hidjsonpath.Text = $"{appExePath}\\Python\\sitestxt.txt";
                    result = $"抽取成功。\r\n PyMsg:{result}\r\n JsondataPath:{hidjsonpath.Text}";
                    //@"F:\NavSite\phantom\0421\data\sitedata.txt"
                }
            }
            catch(Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            capmsg.Text = "抽取中... 开始时间:" + DateTime.Now;
            string url = capurl.Text;
            int count = 0;
            int.TryParse(capcount.Text, out count);
            if (!string.IsNullOrWhiteSpace(url) && count > 0)
            {
                capmsg.Text = _PullData(url, count);
            }
            else
            {
                capmsg.Text = "参数有误";
            }
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                string sitedata = File.ReadAllText(hidjsonpath.Text).Replace("&","");

                var articlelist = JsonConvert.DeserializeObject<List<M_Article>>(sitedata);
                if (articlelist != null && articlelist.Count() > 0)
                {
                    capmsg.Text = $"验证通过,共计{articlelist.Count()}条.";
                 // MessageBox.Show(  $"验证通过,共计{articlelist.Count()}条.");
                }
                else {
                    capmsg.Text = $"验证未通过,请查看文件:{hidjsonpath.Text}.";
                    //MessageBox.Show("验证未通过,请查看文件.");
                }
            }
            catch(Exception ex) {
                capmsg.Text = ex.Message;
            }

        }
        /// <summary>
        /// 推送到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // string url = "http://localhost:8080/article/add/";
            string url = "http://www.isharesite.com/article/add/";
            string datadir = hidjsonpath.Text;
            string sitedata = File.ReadAllText(datadir);
            string param = "token=2CBa31gg4s7dB&entityJson=" + sitedata;
            string result = RequestHelper.PostHttp(url, param);
            if (string.IsNullOrEmpty(result))
            {
                capmsg.Text = "成功";
            }
            else
            { capmsg.Text = result; }
        }

        /// <summary>
        ///  打开json文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(hidjsonpath.Text);
        }
        /// <summary>
        /// 打开网站的文章列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.Links[0].LinkData = "http://www.isharesite.com/Article/1.html";
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void copymsg_Click(object sender, EventArgs e)
        {
            if (capmsg.Text != "")
                Clipboard.SetDataObject(capmsg.Text);
        }
    }
}
