using BetterSite.Domain;
using IBatisNet.DataMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.DataAccess
{
  public  class TagsDA
    {
      ///插入
      public object Insert(M_Tags model)
      {
          return BaseDA.Insert<M_Tags>("Tab_Tags_Insert", model);
      }
      ///更新
      public int Update(M_Tags model)
      {
          return BaseDA.Update<M_Tags>("Tab_Tags_Update", model);
      }
      ///删除
      public int Delete(string entityId)
      {
          return BaseDA.Delete("Tab_Tags_Delete", entityId);
      }
      ///查询对象
      public M_Tags Get(string siteId)
      {
          return BaseDA.Get<M_Tags>("", siteId);
      }
      ///查询列表
      //public IList<M_Sites> QueryForList(M_Sites where)
      //{
      //    return BaseDA.QueryForList<M_Sites>("Tab_Sites_Select", where);
      //}
      public IList QueryForList(M_Tags where)
      {
          return BaseDA.QueryForList("Tab_Tags_Select", where);
      }
      public IList QueryForPageList(M_Tags where)
      {
          return BaseDA.QueryForList("Tab_Tags_SelectPageList", where);
      }
    }
}
