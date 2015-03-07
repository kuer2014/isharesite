using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.Domain
{
    #region M_Types
    /// <summary>
    /// 数据库 [Db_BetterSite] 中表 [dbo.Tab_Types] 的实体类.
    /// </summary>
    /// 创 建 人: {在这里添加创建人}
    /// 创建日期: 2015/2/24
    /// 修 改 人: 
    /// 修改日期:
    /// 修改内容:
    /// 版    本: 1.0.0
    public class M_Types:BaseModel
    {
        private string m_TypeId;

        private string m_Typecode = String.Empty;
        private string m_Typename = String.Empty;
        private int m_Typeclickquantity;
        private int m_Typeordernumber;

        // Instantiate empty M_Types for inserting
        public M_Types() { }

        // Retrieve M_Types with Id for updating
        public M_Types(string TypeId)
        {
            this.m_TypeId = TypeId;
        }

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string TypeId
        {
            get { return m_TypeId; }
            set { m_TypeId = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string TypeCode
        {
            get { return m_Typecode; }
            set { m_Typecode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TypeName
        {
            get { return m_Typename; }
            set { m_Typename = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TypeClickQuantity
        {
            get { return m_Typeclickquantity; }
            set { m_Typeclickquantity = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TypeOrderNumber
        {
            get { return m_Typeordernumber; }
            set { m_Typeordernumber = value; }
        }
        #endregion
    }
    #endregion
	

}
