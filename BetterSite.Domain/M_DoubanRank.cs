using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.Domain
{
    #region M_DoubanRank
    /// <summary>
    /// 数据库 [Db_BetterSite] 中表 [dbo.Tab_DoubanRank] 的实体类.
    /// </summary>
    /// 创 建 人: {在这里添加创建人}
    /// 创建日期: 2017/6/26
    /// 修 改 人: 
    /// 修改日期:
    /// 修改内容:
    /// 版    本: 1.0.0
    public class M_DoubanRank : BaseModel
    {
        private int m_Id;
        private string m_Title;
        private string m_Url;
        private string m_RatingNum;
        private string m_RatingPeople;
        private int m_Category;
        private string m_CreateDate;
        private int m_Status;        
        private int m_Type;

        // Instantiate empty M_Types for inserting
        public M_DoubanRank() { }

        // Retrieve M_Types with Id for updating
        public M_DoubanRank(int Id)
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
        public int Category
        {
            get
            {
                return  m_Category;
            }
            set { m_Category = value; }
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

      

      

        public string Url
        {
            get
            {
                return m_Url;
            }

            set
            {
                m_Url = value;
            }
        }

        public int Type
        {
            get
            {
                return m_Type;
            }

            set
            {
                m_Type = value;
            }
        }

        public string RatingNum
        {
            get
            {
                return m_RatingNum;
            }

            set
            {
                m_RatingNum = value;
            }
        }

        public string RatingPeople
        {
            get
            {
                return m_RatingPeople;
            }

            set
            {
                m_RatingPeople = value;
            }
        }

        #endregion
    }
    #endregion
	

}
