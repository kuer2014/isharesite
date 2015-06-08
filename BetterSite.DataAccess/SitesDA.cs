using BetterSite.Domain;
using IBatisNet.DataMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.DataAccess
{
  public  class SitesDA
    {
      ///插入
      public object Insert(M_Sites model)
      {
          return BaseDA.Insert<M_Sites>("Tab_Sites_Insert", model);
      }
      /// <summary>
      /// 检查后，不存在再插入
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      public object CheckInsert(M_Sites model)
      {
          return BaseDA.Insert<M_Sites>("Tab_Sites_CheckInsert", model);
      }
      ///更新
      public int Update(M_Sites model)
      {
          return BaseDA.Update<M_Sites>("Tab_Sites_Update", model);
      }
      ///删除
      public int Delete(string siteId)
      {
          return BaseDA.Delete("Tab_Sites_Delete", siteId);
      }
      ///查询对象
      public M_Sites Get(string siteId)
      {
          return BaseDA.Get<M_Sites>("", siteId);
      }
      ///查询列表
      //public IList<M_Sites> QueryForList(M_Sites where)
      //{
      //    return BaseDA.QueryForList<M_Sites>("Tab_Sites_Select", where);
      //}
      public IList QueryForList(M_Sites where)
      {
          return BaseDA.QueryForList("Tab_Sites_Select", where);
      }
      public IList QueryForPageList(M_Sites where)
      {
          return BaseDA.QueryForList("Tab_Sites_SelectPageList", where);
      }
      public object QueryForObject(M_Sites where)
      {
          return BaseDA.QueryForObject("Tab_Sites_SelectCount", where);
      }
      public IList QueryForJoinTagList(M_Sites where)
      {
          return BaseDA.QueryForList("Tab_Sites_JoinTag_Select", where);
      }
    }
}
