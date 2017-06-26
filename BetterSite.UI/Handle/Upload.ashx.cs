using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BetterSite.UI.Handle
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string result = string.Empty;
            context.Response.ContentType = "text/plain";
            string op = context.Request["op"] ?? "";
            switch (op.ToLower())
            {

                //case "updatefile": result = UploadFile(context); break;
                case "uploadimage": result = UploadImage(context); break;
                default: result = "参数错误！"; break;
            }
            context.Response.Write(result);
        }
        private string UploadImage(HttpContext context)
        {
            string result = string.Empty;
            string imgPath = "~/temp/";
            string fileName, fileExtension;
            string pic = string.Empty;
            int status = 0;
            string message = string.Empty;
            try
            {
                HttpFileCollection files = context.Request.Files;
                if (files.Count == 0)
                {
                    status = -1;
                    message = "未接收到文件";
                    return JsonResult("", message, status);
                }
                HttpPostedFile postedFile = files[0];
                fileName = Path.GetFileName(postedFile.FileName);
                fileExtension = Path.GetExtension(fileName);
                if (fileExtension.ToLower() != ".jpg" && fileExtension.ToLower() != ".png")
                {
                    status = -1;
                    message = "请上传jpg或png格式文件";
                    return JsonResult("", message, status);
                }
                if (postedFile.ContentLength > 2048 * 1024)
                {
                    status = -1;
                    message = "图片大小不能超过2M！";
                }
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssff") + "_temp" + fileExtension;
                string dir = context.Request.MapPath(imgPath) + newFileName;
                postedFile.SaveAs(dir);
                string src = "/temp/" + newFileName;
                status = 1;
                message = "保存成功";
                return JsonResult(src, message, status);
            }
            catch (Exception e)
            {
                status = -1;
                message = "保存错误：" + e.Message;
                return JsonResult("", message, status);
            }

        }
        private string JsonResult(string content,string message,int status) {
            return "{\"content\": \"" + content + "\",\"message\":\"" + message + "\",\"status\":" + status + "}";
        }
    

    public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}