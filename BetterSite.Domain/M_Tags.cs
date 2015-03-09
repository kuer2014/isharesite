using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.Domain
{
    #region M_Tags
    /// <summary>
    /// 数据库 [Db_BetterSite] 中表 [dbo.Tab_Tags] 的实体类.
    /// </summary>
    /// 创 建 人: {在这里添加创建人}
    /// 创建日期: 2015/2/24
    /// 修 改 人: 
    /// 修改日期:
    /// 修改内容:
    /// 版    本: 1.0.0
    public class M_Tags:BaseModel
    {
        private string m_TagId;

        private string m_Tagcode = String.Empty;
        private string m_Tagname = String.Empty;
        private int m_Tagclickquantity;
        private int m_Tagordernumber;

        // Instantiate empty M_Tags for inserting
        public M_Tags() { }

        // Retrieve M_Tags with Id for updating
        public M_Tags(string TagId)
        {
            this.m_TagId = TagId;
        }

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string TagId
        {
            get { return m_TagId; }
            set { m_TagId = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string TagCode
        {
            get { return m_Tagcode; }
            set { m_Tagcode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TagName
        {
            get { return m_Tagname; }
            set { m_Tagname = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TagClickQuantity
        {
            get { return m_Tagclickquantity; }
            set { m_Tagclickquantity = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TagOrderNumber
        {
            get { return m_Tagordernumber; }
            set { m_Tagordernumber = value; }
        }
        #endregion
    }
    #endregion
	

}
