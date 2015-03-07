using BetterSite.DataAccess;
using BetterSite.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.BusinessObject
{
    public class TypesBO
    {
        #region 单例
        private TypesDA da = null;
        private void InitDA()
        { da = new TypesDA(); }
        public TypesBO()
        {
            //保证只有一个实例
            if (da == null)
            {
                lock (typeof(TypesDA))
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
      public object Insert(M_Types model)
      {
          return da.Insert(model);
      }
      ///更新
      public int Update(M_Types model)
      {
          return da.Update(model);
      }
      ///删除
      public int Delete(string siteId)
      {
          return da.Delete(siteId);
      }
      ///查询对象
      public M_Types Get(string siteId)
      {
          return da.Get(siteId);
      }
      ///查询列表
      //public IList<M_Sites> QueryForList(M_Sites where)
      //{
      //    return da.QueryForList(where);
      //}
      public IList QueryForList(M_Types where)
      {
          return da.QueryForList(where);
      }
      public IList QueryForPageList(M_Types where)
      {
          return da.QueryForPageList(where);
      }
    }
}
