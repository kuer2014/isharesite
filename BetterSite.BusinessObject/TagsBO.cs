using BetterSite.DataAccess;
using BetterSite.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.BusinessObject
{
    public class TagsBO
    {
        #region 单例
        private TagsDA da = null;
        private void InitDA()
        { da = new TagsDA(); }
        public TagsBO()
        {
            //保证只有一个实例
            if (da == null)
            {
                lock (typeof(TagsDA))
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
      public object Insert(M_Tags model)
      {
          return da.Insert(model);
      }
      ///更新
      public int Update(M_Tags model)
      {
          return da.Update(model);
      }
      ///删除
      public int Delete(string entityId)
      {
          return da.Delete(entityId);
      }
      ///查询对象
      public M_Tags Get(string siteId)
      {
          return da.Get(siteId);
      }
      ///查询列表
      //public IList<M_Sites> QueryForList(M_Sites where)
      //{
      //    return da.QueryForList(where);
      //}
      public IList QueryForList(M_Tags where)
      {
          return da.QueryForList(where);
      }
      public IList QueryForPageList(M_Tags where)
      {
          return da.QueryForPageList(where);
      }
    }
}
