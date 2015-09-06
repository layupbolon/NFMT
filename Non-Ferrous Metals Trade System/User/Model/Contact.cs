
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Contact.cs
// 文件功能描述：联系人dbo.Contact实体类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
    /// <summary>
    /// 联系人dbo.Contact实体类。
    /// </summary>
    [Serializable]
    public class Contact : IModel
    {
        #region 字段

        private int contactId;
        private string contactName = String.Empty;
        private string contactCode = String.Empty;
        private string contactTel = String.Empty;
        private string contactFax = String.Empty;
        private string contactAddress = String.Empty;
        private int corpId;
        private Common.StatusEnum contactStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Contact";

        #endregion

        #region 构造函数

        public Contact()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int ContactId
        {
            get { return contactId; }
            set { contactId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ContactName
        {
            get { return contactName; }
            set { contactName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ContactCode
        {
            get { return contactCode; }
            set { contactCode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ContactTel
        {
            get { return contactTel; }
            set { contactTel = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ContactFax
        {
            get { return contactFax; }
            set { contactFax = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ContactAddress
        {
            get { return contactAddress; }
            set { contactAddress = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId
        {
            get { return corpId; }
            set { corpId = value; }
        }

        public string CorpName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum ContactStatus
        {
            get { return contactStatus; }
            set { contactStatus = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CreatorId
        {
            get { return creatorId; }
            set { creatorId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int LastModifyId
        {
            get { return lastModifyId; }
            set { lastModifyId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastModifyTime
        {
            get { return lastModifyTime; }
            set { lastModifyTime = value; }
        }

        /// <summary>
        /// 数据序号
        /// </summary>
        public int Id
        {
            get { return this.contactId; }
            set { this.contactId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return contactStatus; }
            set { contactStatus = value; }
        }

        /// <summary>
        /// 数据状态名
        /// </summary>
        public string ContactStatusName
        {
            get { return this.contactStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.User.DAL.ContactDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.User";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT_User";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}