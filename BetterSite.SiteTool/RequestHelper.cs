using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BetterSite.SiteTool
{
    public class RequestHelper
    {
        #region HttpClient方式
        public static string GetHttp(string url)
        {
            using (var client = new HttpClient())
            {
                var responseString = client.GetStringAsync(url);
                return responseString.Result;
            } 
        }
        /// <summary>
        /// HttpClient 实现,异步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostAsync(string url, string param)
        {
            var client = new HttpClient();
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("param", param));
            // include other fields
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(url, content);
            return response;
        }
        /// <summary>
        /// HttpClient 实现,同步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PostSync(string url, string param)
        {
            var client = new HttpClient();
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("param", param));
            // include other fields
            var content = new FormUrlEncodedContent(values);
          
             var response =  client.PostAsync(url, content);
             response.Wait();//等待 Task 完成执行过程
            return response;
        }
        #endregion HttpClient方式
        #region WebRequest
        public static string GetRequest(string url)
        {
            string result = string.Empty;
            //创建WebRequest
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            //webrequest.UserAgent = "iTunes/12.5.5(Windows; Microsoft Windows 7 x64 Ultimate Edition Service Pack 1(Build 7601); x64) AppleWebKit/7602.4008.0.22";

            //获取返回数据
            HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse();
            //转码
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            //返回的结果
            result = sr.ReadToEnd();
            //关闭
            sr.Close();
            response.Close();
            return result;
        }
        /// <summary>
        /// 轻量版
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string PostHttp(string url, string param)
        {
            byte[] btBodys = Encoding.UTF8.GetBytes(param);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

           httpWebRequest.ContentType = "application/x-www-form-urlencoded";
           // httpWebRequest.ContentType = "text/plain";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 50000;

            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();
            httpWebRequest.Abort();
            httpWebResponse.Close();

            return responseContent;
        } 
        /// <summary>
        ///完整版 post提交数据
        /// </summary>
        /// <param name="formUrl"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public static string PostData(string formUrl, string formData)
        {
            try
            {
                byte[] postData = Encoding.UTF8.GetBytes(formData);

                // 设置提交的相关参数 
                HttpWebRequest request = WebRequest.Create(formUrl) as HttpWebRequest;
                Encoding myEncoding = Encoding.UTF8;
                request.Method = "POST";
                request.KeepAlive = false;
                request.AllowAutoRedirect = true;
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR  3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
                request.ContentLength = postData.Length;

                // 提交请求数据 
                System.IO.Stream outputStream = request.GetRequestStream();
                outputStream.Write(postData, 0, postData.Length);
                outputStream.Close();

                HttpWebResponse response;
                Stream responseStream;
                StreamReader reader;
                string srcString;
                response = request.GetResponse() as HttpWebResponse;
                responseStream = response.GetResponseStream();
                reader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
                srcString = reader.ReadToEnd();
                string result = srcString;   //返回值赋值
                reader.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion WebRequest
        ///// <summary>
        ///// 百度推送  curl推送
        ///// </summary>
        ///// <param name="formUrl"></param>
        ///// <param name="formData"></param>
        ///// <returns></returns>
        //public static string CurlPost(string formUrl, string formData)
        //{
        //    try
        //    {
        //        byte[] postData = Encoding.UTF8.GetBytes(formData);

        //        // 设置提交的相关参数 
        //        HttpWebRequest request = WebRequest.Create(formUrl) as HttpWebRequest;
        //        Encoding myEncoding = Encoding.UTF8;
        //        request.Method = "POST";
        //        request.KeepAlive = false;
        //        request.AllowAutoRedirect = true;
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR  3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
        //        request.ContentLength = postData.Length;

        //        // 提交请求数据 
        //        System.IO.Stream outputStream = request.GetRequestStream();
        //        outputStream.Write(postData, 0, postData.Length);
        //        outputStream.Close();

        //        HttpWebResponse response;
        //        Stream responseStream;
        //        StreamReader reader;
        //        string srcString;
        //        response = request.GetResponse() as HttpWebResponse;
        //        responseStream = response.GetResponseStream();
        //        reader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
        //        srcString = reader.ReadToEnd();
        //        string result = srcString;   //返回值赋值
        //        reader.Close();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
    }
}