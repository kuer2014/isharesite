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

    }
}
