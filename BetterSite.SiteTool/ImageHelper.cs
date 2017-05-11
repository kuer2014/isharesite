using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BetterSite.SiteTool
{
   public class ImageHelper
    {
        /// <summary>
        /// 将图片数据转换为Base64字符串
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static string ToBase64(Image img)
        {
           // Image img = this.pictureBox.Image;
            BinaryFormatter binFormatter = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            binFormatter.Serialize(memStream, img);
            byte[] bytes = memStream.GetBuffer();
            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        /// <summary>
        /// 将Base64字符串转换为图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static Image ToImage(string base64)
        {

            try
            {
                if (base64.IndexOf("data:image") == 0)
                    base64 = base64.Split(',')[1];
                //string base64 = this.richTextBox.Text;
                byte[] bytes = Convert.FromBase64String(base64);
                MemoryStream memStream = new MemoryStream(bytes);
                Bitmap bmp = new Bitmap(memStream);
                memStream.Close();

                return bmp;
            }
            catch {
                return null;
            }
           // BinaryFormatter binFormatter = new BinaryFormatter();
           // Image img = (Image)binFormatter.Deserialize(memStream);
           // return  img;
        }
    }
}
