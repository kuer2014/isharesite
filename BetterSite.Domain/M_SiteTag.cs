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
        private Guid m_SiteTagId;

        private Guid m_Siteid = Guid.Empty;
        private Guid m_Tagid = Guid.Empty;

        // Instantiate empty M_SiteTag for inserting
        public M_SiteTag() { }

        // Retrieve M_SiteTag with Id for updating
        public M_SiteTag(Guid SiteTagId)
        {
            this.m_SiteTagId = SiteTagId;
        }

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public Guid SiteTagId
        {
            get { return m_SiteTagId; }
            set { m_SiteTagId = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public Guid SiteId
        {
            get { return m_Siteid; }
            set { m_Siteid = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid TagId
        {
            get { return m_Tagid; }
            set { m_Tagid = value; }
        }
        #endregion
    }
    #endregion
	

}
