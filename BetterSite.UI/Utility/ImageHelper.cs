using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BetterSite.UI
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
        /// 将Base64字符串转换为图片并返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static Image ToImage(string base64)
        {
            if (base64.IndexOf("data:image") ==0)
                base64 = base64.Split(',')[1];
            //string base64 = this.richTextBox.Text;
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream memStream = new MemoryStream(bytes);
            Bitmap bmp = new Bitmap(memStream);
            memStream.Close();
          
            return bmp;
           // BinaryFormatter binFormatter = new BinaryFormatter();
           // Image img = (Image)binFormatter.Deserialize(memStream);
           // return  img;
        }
        /// <summary>
        /// 将Base64字符串转换为图片保存到本地
        /// </summary>
        /// <param name="fileBody"></param>
        /// <param name="filePath">文件绝对路径</param>
        ///  /// <param name="fileName">文件名，不用带扩展名，可自动识别</param>
        /// <returns>文件绝对路径+文件名</returns>
        public static string Base64ToFile(string fileBody, string filePath,string fileName)
        {
            try
            {
                //if (!System.IO.File.Exists(filePath))
                //{
                //    System.IO.File.Create(filePath);
                //}
                //"data:image/png;base64"文件扩展名获取
                var ext = fileBody.Substring(fileBody.IndexOf('/') + 1, fileBody.IndexOf(';') - fileBody.IndexOf('/') - 1);
                // string filepath = Request.MapPath("~/Upload");
                ///var fileName = Guid.NewGuid().ToString("N") + "." + ext;
                byte[] arr = Convert.FromBase64String(fileBody.Split(',')[1]);
                System.IO.File.WriteAllBytes(filePath + "\\" + fileName + "." + ext, arr);
                return filePath + "\\" + fileName;
            }
            catch (Exception ex)
            {
                //new Common.LogWriter().WriteForWeb(ex);
               // new Common.LogWriter().Write("方法Utility.Base64StringToFile()异常：" + ex);
                return string.Empty;
            }
        }
    }
}
