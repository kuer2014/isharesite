using BetterSite.Domain;
using IBatisNet.DataMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.DataAccess
{
  public  class TypesDA
    {
      ///插入
      public object Insert(M_Types model)
      {
          return BaseDA.Insert<M_Types>("Tab_Types_Insert", model);
      }
      ///更新
      public int Update(M_Types model)
      {
          return BaseDA.Update<M_Types>("Tab_Types_Update", model);
      }
      ///删除
      public int Delete(string siteId)
      {
          return BaseDA.Delete("Tab_Types_Delete", siteId);
      }
      ///查询对象
      public M_Types Get(string siteId)
      {
          return BaseDA.Get<M_Types>("", siteId);
      }
      ///查询列表
      public IList<M_Types> QueryForEntityList(M_Types where)
      {
          return BaseDA.QueryForEntityList<M_Types>("Tab_Types_SelectList", where);
      }
      public IList QueryForList(M_Types where)
      {
          return BaseDA.QueryForList("Tab_Types_Select", where);
      }
      public IList QueryForPageList(M_Types where)
      {
          return BaseDA.QueryForList("Tab_Types_SelectPageList", where);
      }
    }
}
