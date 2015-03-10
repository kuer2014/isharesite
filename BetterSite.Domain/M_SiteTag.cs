using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.Domain
{
    #region M_SiteTag
    /// <summary>
    /// 数据库 [Db_BetterSite] 中表 [dbo.Tab_SiteTag] 的实体类.
    /// </summary>
    /// 创 建 人: {在这里添加创建人}
    /// 创建日期: 2015/2/24
    /// 修 改 人: 
    /// 修改日期:
    /// 修改内容:
    /// 版    本: 1.0.0
    public class M_SiteTag
    {
        private string m_SiteTagId;

        private string m_Siteid = string.Empty;
        private string m_Tagid = string.Empty;

        // Instantiate empty M_SiteTag for inserting
        public M_SiteTag() { }

        // Retrieve M_SiteTag with Id for updating
        public M_SiteTag(string SiteTagId)
        {
            this.m_SiteTagId = SiteTagId;
        }

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string SiteTagId
        {
            get { return m_SiteTagId; }
            set { m_SiteTagId = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string SiteId
        {
            get { return m_Siteid; }
            set { m_Siteid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TagId
        {
            get { return m_Tagid; }
            set { m_Tagid = value; }
        }
        #endregion
    }
    #endregion
	

}
