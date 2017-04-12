using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace BetterSite.UI.Utility
{
 
    public class GetImage
    {
        private int S_Height;
        private int S_Width;
        private int F_Height;
        private int F_Width;
        private string MyURL;
       

        public int ScreenHeight
        {
            get { return S_Height; }
            set { S_Height = value; }
        }

        public int ScreenWidth
        {
            get { return S_Width; }
            set { S_Width = value; }
        }

        public int ImageHeight
        {
            get { return F_Height; }
            set { F_Height = value; }
        }

        public int ImageWidth
        {
            get { return F_Width; }
            set { F_Width = value; }
        }

        public string WebSite
        {
            get { return MyURL; }
            set { MyURL = value; }
        }

        public GetImage(string WebSite, int ScreenWidth, int ScreenHeight, int ImageWidth, int ImageHeight)
        {
            this.WebSite = WebSite;
            this.ScreenWidth = ScreenWidth;
            this.ScreenHeight = ScreenHeight;
            this.ImageHeight = ImageHeight;
            this.ImageWidth = ImageWidth;
        }
        public Bitmap GetBitmap()
        {
            WebPageBitmap Shot = new WebPageBitmap(this.WebSite, this.ScreenWidth, this.ScreenHeight);
            Shot.GetIt();
            Bitmap Pic = Shot.DrawBitmap(this.ImageHeight, this.ImageWidth);
            return Pic;
        }

    }
    class WebPageBitmap
    {
        WebBrowser MyBrowser;
        string URL;
        int Height;
        int Width;

        public WebPageBitmap(string url, int width, int height)
        {
            this.Height = height;
            this.Width = width;
            this.URL = url;
            MyBrowser = new WebBrowser();
            MyBrowser.ScrollBarsEnabled = false;
            MyBrowser.WebBrowserShortcutsEnabled = false;
            MyBrowser.ObjectForScripting = false;
            MyBrowser.Size = new Size(this.Width, this.Height);
            MyBrowser.ScriptErrorsSuppressed = true; //该值指示 WebBrowser 是否禁止显示对话框，如脚本错误消息。
           
  //MyBrowser.IsWebBrowserContextMenuEnabled = false;// 禁用右键菜单
  //          MyBrowser.WebBrowserShortcutsEnabled =false;  //禁用快捷键
  //          MyBrowser.AllowWebBrowserDrop = false;  //获取或设置一个值，该值指示 WebBrowser 控件是否导航到拖放到它上面的文档。
  //                //禁用超链接
  //                //超链接分为两种，一种是 当前窗口直接转向， 一种是 在新窗口中打开
  //               //当然窗口直接转向：
  //              //将 WebBrowser 的 AllowNavigation 设为 false
  //                  //在新窗口中打开：
  //                   //禁用新窗口打开，需要处理 WebBrowser 的 NewWindow 事件
  //                     //private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
  //                      //        {
  //                      //            e.Cancel = true;
  //                    //        }
        }

        public void GetIt()
        {
            MyBrowser.Navigate(this.URL);
            //while (MyBrowser.ReadyState != WebBrowserReadyState.Complete)
            //{
            //    Application.DoEvents();
            //}
            for (int i = 0; i < 1000000; i++)
            {
                if (MyBrowser.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }
                else {
                    break;
                }
            }
        }

        public Bitmap DrawBitmap(int theight, int twidth)
        {
            Bitmap myBitmap = new Bitmap(Width, Height);
            Rectangle DrawRect = new Rectangle(0, 0, Width, Height);
            MyBrowser.DrawToBitmap(myBitmap, DrawRect);
            System.Drawing.Image imgOutput = myBitmap;
            System.Drawing.Image oThumbNail = new Bitmap(twidth, theight, imgOutput.PixelFormat);
            Graphics g = Graphics.FromImage(oThumbNail);
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            Rectangle orectangle = new Rectangle(0, 0, twidth, theight);
            g.DrawImage(imgOutput, orectangle);
            try
            {

                return (Bitmap)oThumbNail;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                imgOutput.Dispose();
                imgOutput = null;
                MyBrowser.Dispose();
                MyBrowser = null;
            }
        }
    }
    public class OLayer
    {
        public string CaptureImage(string url,string fileName)
        {
            string result = string.Empty;
            System.Drawing.Bitmap x = null;
            try
            {
                // string url = "http://" + Request.Url.Host + ":" + Request.Url.Port.ToString(); 
                //url = url + UrlPath; 

                //GetImage thumb = new GetImage(url, 1024, 3200, 1024, 3200);
                //生成页面图片的长宽高
                GetImage thumb = new GetImage(url, 1024, 768, 1024, 768);

                x = thumb.GetBitmap();
               // string FileName = DateTime.Now.ToString("yyyyMMddhhmmss");
                x.Save(fileName);
                //x.Save( "\\cap\\" + FileName + ".jpg");
                //x.Save(System.Environment.CurrentDirectory + FileName + ".jpg");


                //CaptureState = true; 
                result = "ok";
            }
            catch (Exception ex)
            {
                result = ex.Message;
               // BetterSite.
               // MessageBox.Show(ex.ToString());
                //CaptureState = false; 
            }
            finally
            {
                if (x != null) x.Dispose();
            }
            return result;
        }
    }
}