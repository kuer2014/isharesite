using BetterSite.DataAccess;
using BetterSite.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.BusinessObject
{
    public class BlogBO
    {
        #region 单例
        private BlogDA da = null;
        private void InitDA()
        { da = new BlogDA(); }
        public BlogBO()
        {
            //保证只有一个实例
            if (da == null)
            {
                lock (typeof(BlogDA))
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
      public object Insert(M_Blog model)
      {
          return da.Insert(model);
      }
      ///更新
      public int Update(M_Blog model)
      {
          return da.Update(model);
      }
      ///删除
      public int Delete(int id)
      {
          return da.Delete(id);
      }
      ///查询对象
      public M_Blog Get(int id)
      {
          return da.Get(id);
      }
      ///查询列表
      public IList<M_Blog> QueryForEntityList(M_Blog where)
      {
          return da.QueryForEntityList(where);
      }
      public IList QueryForList(M_Blog where)
      {
          return da.QueryForList(where);
      }
      public IList QueryForPageList(M_Blog where)
      {
          return da.QueryForPageList(where);
        }  ///更新点击数
        public int UpdateBlogClickQuantity(int id)
        {
            Hashtable param = new Hashtable();
            param.Add("Id", id);
            return da.UpdateBlogClickQuantity(param);
        }
        public object AddBlogComment(M_BlogComment model)
        {
            return da.AddBlogComment(model);
        }
        public IList QueryBlogCommentForList(M_BlogComment where)
        {
            return da.QueryBlogCommentForList(where);
        }
    }
}
