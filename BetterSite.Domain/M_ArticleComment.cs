using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterSite.Domain
{
    #region M_ArticleComment
    /// <summary>
    /// 数据库 [Db_BetterSite] 中表 [dbo.Tab_Tags] 的实体类.
    /// </summary>
    /// 创 建 人: {在这里添加创建人}
    /// 创建日期: 2015/2/24
    /// 修 改 人: 
    /// 修改日期:
    /// 修改内容:
    /// 版    本: 1.0.0
    public class M_ArticleComment : BaseModel
    {
        private string m_Id;

        private int m_ArticleId;
        private DateTime m_CreateTime;
        private string m_CommentUserNickname = String.Empty;
        private string m_CommentUserIp = String.Empty;
        private string m_CommentContent = String.Empty;
        private int m_Status;

        // Instantiate empty M_Tags for inserting
        public M_ArticleComment() { }

        // Retrieve M_Tags with Id for updating
        public M_ArticleComment(string Id)
        {
            this.m_Id = Id;
        }

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int ArticleId
        {
            get { return m_ArticleId; }
            set { m_ArticleId = value; }
        }
        public DateTime CreateTime
        {
            get
            {
                return m_CreateTime;
            }

            set
            {
                m_CreateTime = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CommentUserNickname
        {
            get { return m_CommentUserNickname; }
            set { m_CommentUserNickname = value; }
        }
   public string CommentUserIp
        {
            get { return m_CommentUserIp; }
            set { m_CommentUserIp = value; }
        }
        public string CommentContent
        {
            get { return m_CommentContent; }
            set { m_CommentContent = value; }
        }
     

        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

     
        #endregion
    }
    #endregion
	

}
