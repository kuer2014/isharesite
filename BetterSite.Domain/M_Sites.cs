using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.Domain
{
        #region M_Sites
        /// <summary>
        /// 数据库 [Db_BetterSite] 中表 [dbo.Tab_Sites] 的实体类.
        /// </summary>
        /// 创 建 人: {在这里添加创建人}
        /// 创建日期: 2015/2/24
        /// 修 改 人: 
        /// 修改日期:
        /// 修改内容:
        /// 版    本: 1.0.0
        public class M_Sites
        {
            private Guid m_SiteId;

            private string m_Sitecode = String.Empty;
            private string m_Sitename = String.Empty;
            private string m_Siteurl = String.Empty;
            private Guid m_Typeid = Guid.Empty;
            private DateTime m_Siteadddate;
            private bool m_Siteistop;
            private bool m_Siteishome;
            private int m_Siteclickquantity;
            private int m_Sitefavoritequantity;
            private int m_Siteapprovequantity;
            private int m_Siteordernumber;
            private bool m_Siteisactive;

            // Instantiate empty M_Sites for inserting
            public M_Sites() { }

            // Retrieve M_Sites with Id for updating
            public M_Sites(Guid SiteId)
            {
                this.m_SiteId = SiteId;
            }

            #region Public Properties

            /// <summary>
            /// 
            /// </summary>
            public Guid SiteId
            {
                get { return m_SiteId; }
                set { m_SiteId = value; }
            }


            /// <summary>
            /// 
            /// </summary>
            public string SiteCode
            {
                get { return m_Sitecode; }
                set { m_Sitecode = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public string SiteName
            {
                get { return m_Sitename; }
                set { m_Sitename = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public string SiteUrl
            {
                get { return m_Siteurl; }
                set { m_Siteurl = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public Guid TypeId
            {
                get { return m_Typeid; }
                set { m_Typeid = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public DateTime SiteAddDate
            {
                get { return m_Siteadddate; }
                set { m_Siteadddate = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public bool SiteIsTop
            {
                get { return m_Siteistop; }
                set { m_Siteistop = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public bool SiteIsHome
            {
                get { return m_Siteishome; }
                set { m_Siteishome = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public int SiteClickQuantity
            {
                get { return m_Siteclickquantity; }
                set { m_Siteclickquantity = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public int SiteFavoriteQuantity
            {
                get { return m_Sitefavoritequantity; }
                set { m_Sitefavoritequantity = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public int SiteApproveQuantity
            {
                get { return m_Siteapprovequantity; }
                set { m_Siteapprovequantity = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public int SiteOrderNumber
            {
                get { return m_Siteordernumber; }
                set { m_Siteordernumber = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public bool SiteIsActive
            {
                get { return m_Siteisactive; }
                set { m_Siteisactive = value; }
            }
            #endregion
        }
        #endregion
}
