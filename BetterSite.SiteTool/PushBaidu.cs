using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetterSite.SiteTool
{
    public partial class PushBaidu : Form
    {
        public PushBaidu()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 推送到百度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string baiduurl = "http://data.zz.baidu.com/urls?site=www.isharesite.com&token=hzM0UEGaAryxFNUx";
            string pushurls = txturls.Text;//"http://www.isharesite.com/Sites/SITE1451619620817\nhttp://www.isharesite.com/Sites/SITE1489735778357" ;

            string msg = "";
            msg = RequestHelper.PostData(baiduurl, pushurls);
            pushmsg.Text = msg;
        }
    }
}
