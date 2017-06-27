using BetterSite.Domain;
using IBatisNet.DataMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.DataAccess
{
  public  class ArticleDA
    {
      ///插入
      public object Insert(M_Article model)
      {
          return BaseDA.Insert<M_Article>("Tab_Article_Insert", model);
      }
      ///更新
      public int Update(M_Article model)
      {
          return BaseDA.Update<M_Article>("Tab_Article_Update", model);
      }
      ///删除
      public int Delete(int id)
      {
          return BaseDA.Delete("Tab_Article_Delete", id+"");
      }
      ///查询对象
      public M_Article Get(int id)
      {
          return BaseDA.Get<M_Article>("Tab_Article_SelectEntity", id+"");
      }
      ///查询列表
      public IList<M_Article> QueryForEntityList(M_Article where)
      {
          return BaseDA.QueryForEntityList<M_Article>("Tab_Article_SelectList", where);
      }
      public IList QueryForList(M_Article where)
      {
          return BaseDA.QueryForList("Tab_Article_Select", where);
      }
      public IList QueryForPageList(M_Article where)
      {
          return BaseDA.QueryForList("Tab_Article_SelectPageList", where);
      }
    }
}
