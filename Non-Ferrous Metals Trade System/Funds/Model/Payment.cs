
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Payment.cs
// 文件功能描述：财务付款dbo.Fun_Payment实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月24日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 财务付款dbo.Fun_Payment实体类。
    /// </summary>
    [Serializable]
    public class Payment : IModel
    {
        #region 字段

        private int paymentId;
        private int payApplyId;
        private string paymentCode = String.Empty;
        private decimal payBala;
        private decimal fundsBala;
        private decimal virtualBala;
        private int currencyId;
        private int payStyle;
        private int payBankId;
        private int payBankAccountId;
        private int payCorp;
        private int payDept;
        private int payEmpId;
        private DateTime payDatetime;
        private int recevableCorp;
        private int receBankId;
        private int receBankAccountId;
        private string receBankAccount = String.Empty;
        private StatusEnum paymentStatus;
        private string flowName = String.Empty;
        private string memo = String.Empty;
        private int fundsLogId;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Fun_Payment";
        #endregion

        #region 构造函数

        public Payment()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 付款序号
        /// </summary>
        public int PaymentId
        {
            get { return paymentId; }
            set { paymentId = value; }
        }

        /// <summary>
        /// 付款申请序号
        /// </summary>
        public int PayApplyId
        {
            get { return payApplyId; }
            set { payApplyId = value; }
        }

        /// <summary>
        /// 付款编号
        /// </summary>
        public string PaymentCode
        {
            get { return paymentCode; }
            set { paymentCode = value; }
        }

        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal PayBala
        {
            get { return payBala; }
            set { payBala = value; }
        }

        /// <summary>
        /// 财务付款金额
        /// </summary>
        public decimal FundsBala
        {
            get { return fundsBala; }
            set { fundsBala = value; }
        }

        /// <summary>
        /// 虚拟付款金额
        /// </summary>
        public decimal VirtualBala
        {
            get { return virtualBala; }
            set { virtualBala = value; }
        }

        /// <summary>
        /// 付款币种
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }

        /// <summary>
        /// 付款方式
        /// </summary>
        public int PayStyle
        {
            get { return payStyle; }
            set { payStyle = value; }
        }

        /// <summary>
        /// 付款银行
        /// </summary>
        public int PayBankId
        {
            get { return payBankId; }
            set { payBankId = value; }
        }

        /// <summary>
        /// 付款账户
        /// </summary>
        public int PayBankAccountId
        {
            get { return payBankAccountId; }
            set { payBankAccountId = value; }
        }

        /// <summary>
        /// 付款公司
        /// </summary>
        public int PayCorp
        {
            get { return payCorp; }
            set { payCorp = value; }
        }

        /// <summary>
        /// 付款部门
        /// </summary>
        public int PayDept
        {
            get { return payDept; }
            set { payDept = value; }
        }

        /// <summary>
        /// 付款操作人
        /// </summary>
        public int PayEmpId
        {
            get { return payEmpId; }
            set { payEmpId = value; }
        }

        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime PayDatetime
        {
            get { return payDatetime; }
            set { payDatetime = value; }
        }

        /// <summary>
        /// 收款公司
        /// </summary>
        public int RecevableCorp
        {
            get { return recevableCorp; }
            set { recevableCorp = value; }
        }

        /// <summary>
        /// 收款银行
        /// </summary>
        public int ReceBankId
        {
            get { return receBankId; }
            set { receBankId = value; }
        }

        /// <summary>
        /// 收款银行账户
        /// </summary>
        public int ReceBankAccountId
        {
            get { return receBankAccountId; }
            set { receBankAccountId = value; }
        }

        /// <summary>
        /// 收款账户
        /// </summary>
        public string ReceBankAccount
        {
            get { return receBankAccount; }
            set { receBankAccount = value; }
        }

        /// <summary>
        /// 付款状态
        /// </summary>
        public StatusEnum PaymentStatus
        {
            get { return paymentStatus; }
            set { paymentStatus = value; }
        }

        /// <summary>
        /// 外部流水号
        /// </summary>
        public string FlowName
        {
            get { return flowName; }
            set { flowName = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        /// <summary>
        /// 资金流水序号
        /// </summary>
        public int FundsLogId
        {
            get { return fundsLogId; }
            set { fundsLogId = value; }
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
            get { return this.paymentId; }
            set { this.paymentId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return paymentStatus; }
            set { paymentStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Funds.DAL.PaymentDAL";
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