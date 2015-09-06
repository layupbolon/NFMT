
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Receivable.cs
// 文件功能描述：收款dbo.Fun_Receivable实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月27日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 收款dbo.Fun_Receivable实体类。
    /// </summary>
    [Serializable]
    public class Receivable : IModel
    {
        #region 字段

        private int receivableId;
        private int receiveEmpId;
        private DateTime receiveDate;
        private int receivableGroupId;
        private int receivableCorpId;
        private int currencyId;
        private decimal payBala;
        private int receivableBank;
        private int receivableAccoontId;
        private int payGroupId;
        private int payCorpId;
        private string payCorpName = String.Empty;
        private int payBankId;
        private string payBank = String.Empty;
        private int payAccountId;
        private string payAccount = String.Empty;
        private string payWord = String.Empty;
        private string bankLog = String.Empty;
        private Common.StatusEnum receiveStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Fun_Receivable";
        #endregion

        #region 构造函数

        public Receivable()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 收款序号
        /// </summary>
        public int ReceivableId
        {
            get { return receivableId; }
            set { receivableId = value; }
        }

        /// <summary>
        /// 收款登记人
        /// </summary>
        public int ReceiveEmpId
        {
            get { return receiveEmpId; }
            set { receiveEmpId = value; }
        }

        /// <summary>
        /// 收款日期
        /// </summary>
        public DateTime ReceiveDate
        {
            get { return receiveDate; }
            set { receiveDate = value; }
        }

        /// <summary>
        /// 收款集团序号
        /// </summary>
        public int ReceivableGroupId
        {
            get { return receivableGroupId; }
            set { receivableGroupId = value; }
        }

        /// <summary>
        /// 收款公司序号
        /// </summary>
        public int ReceivableCorpId
        {
            get { return receivableCorpId; }
            set { receivableCorpId = value; }
        }

        /// <summary>
        /// 收款币种
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }

        /// <summary>
        /// 收款金额
        /// </summary>
        public decimal PayBala
        {
            get { return payBala; }
            set { payBala = value; }
        }

        /// <summary>
        /// 收款银行
        /// </summary>
        public int ReceivableBank
        {
            get { return receivableBank; }
            set { receivableBank = value; }
        }

        /// <summary>
        /// 收款银行账户序号
        /// </summary>
        public int ReceivableAccoontId
        {
            get { return receivableAccoontId; }
            set { receivableAccoontId = value; }
        }

        /// <summary>
        /// 付款集团序号
        /// </summary>
        public int PayGroupId
        {
            get { return payGroupId; }
            set { payGroupId = value; }
        }

        /// <summary>
        /// 付款公司序号
        /// </summary>
        public int PayCorpId
        {
            get { return payCorpId; }
            set { payCorpId = value; }
        }

        /// <summary>
        /// 付款公司名称
        /// </summary>
        public string PayCorpName
        {
            get { return payCorpName; }
            set { payCorpName = value; }
        }

        /// <summary>
        /// 付款银行序号
        /// </summary>
        public int PayBankId
        {
            get { return payBankId; }
            set { payBankId = value; }
        }

        /// <summary>
        /// 付款银行
        /// </summary>
        public string PayBank
        {
            get { return payBank; }
            set { payBank = value; }
        }

        /// <summary>
        /// 付款银行账户序号
        /// </summary>
        public int PayAccountId
        {
            get { return payAccountId; }
            set { payAccountId = value; }
        }

        /// <summary>
        /// 付款银行账户
        /// </summary>
        public string PayAccount
        {
            get { return payAccount; }
            set { payAccount = value; }
        }

        /// <summary>
        /// 简短附言
        /// </summary>
        public string PayWord
        {
            get { return payWord; }
            set { payWord = value; }
        }

        /// <summary>
        /// 外部流水备注
        /// </summary>
        public string BankLog
        {
            get { return bankLog; }
            set { bankLog = value; }
        }

        /// <summary>
        /// 收款状态
        /// </summary>
        public Common.StatusEnum ReceiveStatus
        {
            get { return receiveStatus; }
            set { receiveStatus = value; }
        }

        /// <summary>
        /// 创建人序号
        /// </summary>
        public int CreatorId
        {
            get { return creatorId; }
            set { creatorId = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 最后修改人序号
        /// </summary>
        public int LastModifyId
        {
            get { return lastModifyId; }
            set { lastModifyId = value; }
        }

        /// <summary>
        /// 最后修改时间
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
            get { return this.receivableId; }
            set { this.receivableId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return receiveStatus; }
            set { receiveStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Funds.DAL.ReceivableDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Funds";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}