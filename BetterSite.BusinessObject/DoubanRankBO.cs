using BetterSite.DataAccess;
using BetterSite.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.BusinessObject
{
    public class DoubanRankBO
    {
        #region 单例
        private DoubanRankDA da = null;
        private void InitDA()
        { da = new DoubanRankDA(); }
        public DoubanRankBO()
        {
            //保证只有一个实例
            if (da == null)
            {
                lock (typeof(DoubanRankDA))
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
      public object Insert(M_DoubanRank model)
      {
          return da.Insert(model);
      }
      ///更新
      public int Update(M_DoubanRank model)
      {
          return da.Update(model);
      }  
      ///删除
      public int Delete(int id)
      {
          return da.Delete(id);
      }

      ///查询列表
      public IList<M_DoubanRank> QueryForEntityList(M_DoubanRank where)
      {
          return da.QueryForEntityList(where);
      }
      public IList QueryForList(M_DoubanRank where)
      {
          return da.QueryForList(where);
      }
    
    }
}
