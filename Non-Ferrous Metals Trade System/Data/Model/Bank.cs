
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Bank.cs
// 文件功能描述：银行dbo.Bank实体类。
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
    /// <summary>
    /// 银行dbo.Bank实体类。
    /// </summary>
    [Serializable]
    public class Bank : IModel
    {
        #region 字段

        private int bankId;
        private string bankName = String.Empty;
        private string bankEname = String.Empty;
        private string bankFullName = String.Empty;
        private string bankShort = String.Empty;
        private int capitalType;
        private int bankLevel;
        private bool switchBack;
        private int parentId;
        private Common.StatusEnum bankStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Bank";
        #endregion

        #region 构造函数

        public Bank()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int BankId
        {
            get { return bankId; }
            set { bankId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BankEname
        {
            get { return bankEname; }
            set { bankEname = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BankFullName
        {
            get { return bankFullName; }
            set { bankFullName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BankShort
        {
            get { return bankShort; }
            set { bankShort = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CapitalType
        {
            get { return capitalType; }
            set { capitalType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int BankLevel
        {
            get { return bankLevel; }
            set { bankLevel = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool SwitchBack
        {
            get { return switchBack; }
            set { switchBack = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum BankStatus
        {
            get { return bankStatus; }
            set { bankStatus = value; }
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
            get { return this.bankId; }
            set { this.bankId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return bankStatus; }
            set { bankStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.BankDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Data";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT_Basic";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}