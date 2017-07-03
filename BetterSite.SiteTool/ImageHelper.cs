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
        #region 用法1
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
        #endregion 用法1
        #region 用法2
        //图片 转为    base64编码的文本
        public static string ImgToBase64String(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);
                // this.pictureBox1.Image = bmp;
                FileStream fs = new FileStream(Imagefilename, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                String strbaser64 = Convert.ToBase64String(arr);
                sw.Write(strbaser64);

                sw.Close();
                fs.Close();
                return strbaser64;
                // MessageBox.Show("转换成功!");
            }
            catch (Exception ex)
            {
                //Common.Log4NetHelper.Debug("ImgToBase64String 转换失败\nException:" + ex.Message);
                return "";
            }
        }
        /// <summary>
        /// Base64格式的文体保存到本地
        /// </summary>
        /// <param name="fileBody"> base64编码的文本</param>
        /// <param name="filePath">文件绝对路径；文件名称为GUID</param>
        /// <returns>文件名</returns>
        public static string Base64StringToFile(string fileBody, string filePath)
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
                var fileName = Guid.NewGuid().ToString("N") + "." + ext;
                byte[] arr = Convert.FromBase64String(fileBody.Split(',')[1]);
                System.IO.File.WriteAllBytes(filePath + "\\" + fileName, arr);
                // return filePath + "\\" + fileName;
                return fileName;
            }
            catch (Exception ex)
            {                
               // Common.Log4NetHelper.Debug("Base64StringToFile 转换失败\nException:" + ex.Message);
                return string.Empty;
            }
        }
        /// <summary>
        ///  Base64格式的文体保存到磁盘
        /// </summary>
        /// <param name="fileBody"> base64编码的文本</param>
        /// <param name="filePath">绝对地址</param>
        /// <param name="fileName">不用带扩展名</param>
        /// <param name="fileExt">不指定时从base64字符串中取</param>
        /// <returns></returns>
        public static string Base64StringToFile(string fileBody, string filePath,string fileName,string fileExt="")
        {
            try
            {
                //if (!System.IO.File.Exists(filePath))
                //{
                //    System.IO.File.Create(filePath);
                //}
                if (string.IsNullOrWhiteSpace(fileExt)) { 
                //"data:image/png;base64"文件扩展名获取
                fileExt = fileBody.Substring(fileBody.IndexOf('/') + 1, fileBody.IndexOf(';') - fileBody.IndexOf('/') - 1);
                }
                // string filepath = Request.MapPath("~/Upload");
                fileName = fileName + "." + fileExt;
                byte[] arr = Convert.FromBase64String(fileBody.Split(',')[1]);
                System.IO.File.WriteAllBytes(filePath + "\\" + fileName, arr);
                // return filePath + "\\" + fileName;
                return fileName;
            }
            catch (Exception ex)
            {
                // Common.Log4NetHelper.Debug("Base64StringToFile 转换失败\nException:" + ex.Message);
                return string.Empty;
            }
        }
        #endregion 用法2
    }
}
