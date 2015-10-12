using BetterSite.DataAccess;
using BetterSite.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.BusinessObject
{
    public class SiteTagBO
    {
        #region 单例
        private SiteTagDA da = null;
        private void InitDA()
        { da = new SiteTagDA(); }
        public SiteTagBO()
        {
            //保证只有一个实例
            if (da == null)
            {
                lock (typeof(SiteTagDA))
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
      public object Insert(M_SiteTag model)
      {
          return da.Insert(model);
      }
      ///更新
      public int Update(M_SiteTag model)
      {
          return da.Update(model);
      }
      ///删除
      public int Delete(string entityId)
      {
          return da.Delete(entityId);
      }
       /// <summary>
      ///根据siteId 删除
       /// </summary>
       /// <param name="entityId"></param>
       /// <returns></returns>
      public int DeleteBySiteId(string siteId)
      {
          return da.DeleteBySiteId(siteId);
      }
      ///查询对象
      public M_SiteTag Get(string siteId)
      {
          return da.Get(siteId);
      }
      ///查询列表
      //public IList<M_Sites> QueryForList(M_Sites where)
      //{
      //    return da.QueryForList(where);
      //}
      public IList QueryForList(M_SiteTag where)
      {
          return da.QueryForList(where);
      }
      public IList QueryForListByTags(Hashtable where)
      {
          return da.QueryForListByTags(where);
      }
      public IList QueryForPageList(M_SiteTag where)
      {
          return da.QueryForPageList(where);
      }
    }
}
