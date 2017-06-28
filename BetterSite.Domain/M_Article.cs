using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.Domain
{
    #region M_Article
    /// <summary>
    /// 数据库 [Db_BetterSite] 中表 [dbo.Tab_Article] 的实体类.
    /// </summary>
    /// 创 建 人: {在这里添加创建人}
    /// 创建日期: 2017/6/26
    /// 修 改 人: 
    /// 修改日期:
    /// 修改内容:
    /// 版    本: 1.0.0
    public class M_Article : BaseModel
    {
        private int m_Id;

        private int? m_Category;
        private string m_Content = String.Empty;
        private string m_CreateDate;
        private int m_Status;
        private string m_Title;
        private int m_PageView;
        private string m_Description;
        private string m_CategoryName;
        // Instantiate empty M_Types for inserting
        public M_Article() { }

        // Retrieve M_Types with Id for updating
        public M_Article(int Id)
        {
            this.m_Id = Id;
        }

        #region Public Properties
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Category
        {
            get
            {
                return  m_Category;
            }
            set { m_Category = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Content
        {
            get { return m_Content; }
            set { m_Content = value; }
        }

        public string CreateDate
        {
            get
            {
                return m_CreateDate;
            }

            set
            {
                m_CreateDate = value;
            }
        }

        public int Status
        {
            get
            {
                return m_Status;
            }

            set
            {
                m_Status = value;
            }
        }

        public string Title
        {
            get
            {
                return m_Title;
            }

            set
            {
                m_Title = value;
            }
        }

        public int PageView
        {
            get
            {
                return m_PageView;
            }

            set
            {
                m_PageView = value;
            }
        }

        public string Description
        {
            get
            {
                return m_Description;
            }

            set
            {
                m_Description = value;
            }
        }

        public string CategoryName
        {
            get
            {
                return m_CategoryName;
            }

            set
            {
                m_CategoryName = value;
            }
        }

        #endregion
    }
    #endregion
	

}
