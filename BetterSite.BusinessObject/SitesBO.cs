using BetterSite.DataAccess;
using BetterSite.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.BusinessObject
{
    public class SitesBO
    {
        #region 单例
        private SitesDA da = null;
        private void InitDA()
        { da = new SitesDA(); }
        public SitesBO()
        {
            //保证只有一个实例
            if (da == null)
            {
                lock (typeof(SitesDA))
                {
                    if (da == null)
                    {
                        InitDA();
                    }
                }
            }
        }
        #endregion
        ///插入
      public object Insert(M_Sites model)
      {
          return da.Insert(model);
      }
      /// <summary>
      /// 检查后，不存在再插入
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      public object CheckInsert(M_Sites model)
      {
          return da.CheckInsert(model);
      }
      ///更新
      public int Update(M_Sites model)
      {
          return da.Update(model);
      }
      ///删除
      public int Delete(string siteId)
      {
          return da.Delete(siteId);
      }
      ///查询对象
      public M_Sites Get(string siteId)
      {
          return da.Get(siteId);
      }
      ///查询列表
      //public IList<M_Sites> QueryForList(M_Sites where)
      //{
      //    return da.QueryForList(where);
      //}
      public IList QueryForList(M_Sites where)
      {
          return da.QueryForList(where);
      }
      public IList QueryForPageList(M_Sites where)
      {
          return da.QueryForPageList(where);
      }
      public object QueryForObject(M_Sites where)
      {
          return da.QueryForObject(where);
      }
      public IList QueryForJoinTagList(M_Sites where)
      {
          return da.QueryForJoinTagList(where);
      }
    }
}
