using BetterSite.Domain;
using IBatisNet.DataMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.DataAccess
{
  public  class DoubanRankDA
    {
      ///插入
      public object Insert(M_DoubanRank model)
      {
          return BaseDA.Insert<M_DoubanRank>("Tab_DoubanRank_Insert", model);
      }
      ///更新
      public int Update(M_DoubanRank model)
      {
          return BaseDA.Update<M_DoubanRank>("Tab_DoubanRank_Update", model);
      }
      ///删除
      public int Delete(int id)
      {
          return BaseDA.Delete("Tab_DoubanRank_Delete", id+"");
      }
      ///查询列表
      public IList<M_DoubanRank> QueryForEntityList(M_DoubanRank where)
      {
          return BaseDA.QueryForEntityList<M_DoubanRank>("Tab_DoubanRank_Select", where);
      }
      public IList QueryForList(M_DoubanRank where)
      {
          return BaseDA.QueryForList("Tab_DoubanRank_Select", where);
      }
    
    }
}
