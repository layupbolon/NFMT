
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Corporation.cs
// 文件功能描述：公司dbo.Corporation实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月7日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
    /// <summary>
    /// 公司dbo.Corporation实体类。
    /// </summary>
    [Serializable]
    public class Corporation : IModel
    {
        #region 字段

        private int corpId;
        private int parentId;
        private string corpCode = String.Empty;
        private string corpName = String.Empty;
        private string corpEName = String.Empty;
        private string taxPayerId;
        private string corpFullName = String.Empty;
        private string corpFullEName = String.Empty;
        private string corpAddress = String.Empty;
        private string corpEAddress = String.Empty;
        private string corpTel = String.Empty;
        private string corpFax = String.Empty;
        private string corpZip = String.Empty;
        private int corpType;
        private Common.StatusEnum corpStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Corporation";

        #endregion

        #region 构造函数

        public Corporation()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int CorpId
        {
            get { return corpId; }
            set { corpId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        public string BlocName { get; set; }

        public bool IsSelf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CorpCode
        {
            get { return corpCode; }
            set { corpCode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CorpName
        {
            get { return corpName; }
            set { corpName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CorpEName
        {
            get { return corpEName; }
            set { corpEName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TaxPayerId
        {
            get { return taxPayerId; }
            set { taxPayerId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CorpFullName
        {
            get { return corpFullName; }
            set { corpFullName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CorpFullEName
        {
            get { return corpFullEName; }
            set { corpFullEName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CorpAddress
        {
            get { return corpAddress; }
            set { corpAddress = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CorpEAddress
        {
            get { return corpEAddress; }
            set { corpEAddress = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CorpTel
        {
            get { return corpTel; }
            set { corpTel = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CorpFax
        {
            get { return corpFax; }
            set { corpFax = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CorpZip
        {
            get { return corpZip; }
            set { corpZip = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CorpType
        {
            get { return corpType; }
            set { corpType = value; }
        }

        public string CorpTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum CorpStatus
        {
            get { return corpStatus; }
            set { corpStatus = value; }
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
            get { return this.corpId; }
            set { this.corpId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return corpStatus; }
            set { corpStatus = value; }
        }

        /// <summary>
        /// 数据状态名
        /// </summary>
        public string CorpStatusName
        {
            get { return this.corpStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.User.DAL.CorporationDAL";
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