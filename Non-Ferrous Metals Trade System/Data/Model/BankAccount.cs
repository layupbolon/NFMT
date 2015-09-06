
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BankAccount.cs
// 文件功能描述：银行账号dbo.BankAccount实体类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
    /// <summary>
    /// 银行账号dbo.BankAccount实体类。
    /// </summary>
    [Serializable]
    public class BankAccount : IModel
    {
        #region 字段

        private int bankAccId;
        private int companyId;
        private int bankId;
        private string accountNo = String.Empty;
        private int currencyId;
        private string bankAccDesc = String.Empty;
        private Common.StatusEnum bankAccStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.BankAccount";

        #endregion

        #region 构造函数

        public BankAccount()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int BankAccId
        {
            get { return bankAccId; }
            set { bankAccId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CorpName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BankId
        {
            get { return bankId; }
            set { bankId = value; }
        }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }
        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BankAccDesc
        {
            get { return bankAccDesc; }
            set { bankAccDesc = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum BankAccStatus
        {
            get { return bankAccStatus; }
            set { bankAccStatus = value; }
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
            get { return this.bankAccId; }
            set { this.bankAccId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return bankAccStatus; }
            set { bankAccStatus = value; }
        }
        /// <summary>
        /// 数据状态名
        /// </summary>
        public string BankAccStatusName
        {
            get { return this.bankAccStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.BankAccountDAL";
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