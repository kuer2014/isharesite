using BetterSite.Domain;
using IBatisNet.DataMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.DataAccess
{
  public  class SiteTagDA
    {
      ///插入
      public object Insert(M_SiteTag model)
      {
          return BaseDA.Insert<M_SiteTag>("Tab_SiteTag_Insert", model);
      }
      ///更新
      public int Update(M_SiteTag model)
      {
          return BaseDA.Update<M_SiteTag>("Tab_SiteTag_Update", model);
      }
      ///删除
      public int Delete(string entityId)
      {
          return BaseDA.Delete("Tab_SiteTag_Delete", entityId);
      }
      /// <summary>
      ///根据siteId 删除
      /// </summary>
      /// <param name="entityId"></param>
      /// <returns></returns>
      public int DeleteBySiteId(string siteId)
      {
          return BaseDA.Delete("Tab_SiteTag_DeleteBySiteId", siteId);
      }
      ///查询对象
      public M_SiteTag Get(string siteId)
      {
          return BaseDA.Get<M_SiteTag>("", siteId);
      }
      ///查询列表
      //public IList<M_Sites> QueryForList(M_Sites where)
      //{
      //    return BaseDA.QueryForList<M_Sites>("Tab_Sites_Select", where);
      //}
      public IList QueryForList(M_SiteTag where)
      {
          return BaseDA.QueryForList("Tab_SiteTag_Select", where);
      }
      public IList QueryForListByTags(Hashtable where)
      {
          return BaseDA.QueryForList("Tab_SiteTag_SelectSiteIdByTag", where);
      }
      public IList QueryForPageList(M_SiteTag where)
      {
          return BaseDA.QueryForList("Tab_SiteTag_SelectPageList", where);
      }
    }
}
