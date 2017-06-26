using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.Common
{
    class DataStuct_Example
    {
        #region 字典 Dictionary
        void dicFunc()
        {
            //字典值存不同类型数据
            var result = new Dictionary<string, dynamic>();
            result.Add("op", "激活回调");
            result.Add("status", -2);
        }
        #endregion
        ///动态类型
        /// <summary>
        ///返回值。可以省去新建一个类，转json后为 {    "parameter1": "LSApplicationWorkspace",    "parameter2": "defaultWorkspace"  }
        /// </summary>
        /// <returns></returns>
        dynamic Get()
        {
            return new
            {
                parameter1 = "LSApplicationWorkspace",
                parameter2 = "defaultWorkspace"
            };
        }
        /// <summary>
        /// 返回字典结构数据列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<dynamic, dynamic> GetApp()
        {

            //using (IDbConnection connection = new MySqlConnection(DbConnString))
            //{
            //    connection.Open();
            //    var result = connection.Query("SELECT Id,`Name` FROM app WHERE `Status`=1;").ToDictionary(key => key.Id, value => value.Name);
            //    return result;
            //}
            return new Dictionary<dynamic, dynamic>();//使用时去掉此句，取消注释上边代码
        }

    }
}
