using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.Common
{
    class 设计模式实例
    {
    }
    #region 单例模式
    public class AdvertiserBI
    {
        #region 类实例化单件模式
        //构造函数私有化
        private AdvertiserBI() { }
        static AdvertiserBI instance = null;
        static readonly object padlock = new object();
        public static AdvertiserBI Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new AdvertiserBI();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion
    }
    #endregion 单例模式
}
