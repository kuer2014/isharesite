using BetterSite.Domain;
using IBatisNet.DataMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.DataAccess
{
  public  class BlogDA
    {
      ///插入
      public object Insert(M_Blog model)
      {
          return BaseDA.Insert<M_Blog>("Tab_Blog_Insert", model);
      }
      ///更新
      public int Update(M_Blog model)
      {
          return BaseDA.Update<M_Blog>("Tab_Blog_Update", model);
      } 
        ///更新
      public int UpdateStatus(M_Blog model)
      {
          return BaseDA.Update<M_Blog>("Tab_Blog_UpdateStatus", model);
      }
      ///删除
      public int Delete(int id)
      {
          return BaseDA.Delete("Tab_Blog_Delete", id+"");
      }
      ///查询对象
      public M_Blog Get(int id)
      {
          return BaseDA.Get<M_Blog>("Tab_Blog_SelectEntity", id+"");
      }
      ///查询列表
      public IList<M_Blog> QueryForEntityList(M_Blog where)
      {
          return BaseDA.QueryForEntityList<M_Blog>("Tab_Blog_SelectList", where);
      }
      public IList QueryForList(M_Blog where)
      {
          return BaseDA.QueryForList("Tab_Blog_Select", where);
      }
      public IList QueryForPageList(M_Blog where)
      {
          return BaseDA.QueryForList("Tab_Blog_SelectPageList", where);
      }
        ///更新点击数
        public int UpdateBlogClickQuantity(Hashtable param)
        {
            return BaseDA.Update<Hashtable>("Tab_Sites_UpdateBlogClickQuantity", param);
        }
        public object AddBlogComment(M_BlogComment model)
        {
            return BaseDA.Insert<M_BlogComment>("Tab_BlogComment_Insert", model);
        }
        public IList QueryBlogCommentForList(M_BlogComment where)
        {
            return BaseDA.QueryForList("Tab_BlogComment_Select", where);
        }
    }
}
