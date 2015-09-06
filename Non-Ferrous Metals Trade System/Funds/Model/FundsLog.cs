
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FundsLog.cs
// 文件功能描述：资金流水dbo.Fun_FundsLog实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月22日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 资金流水dbo.Fun_FundsLog实体类。
    /// </summary>
    [Serializable]
    public class FundsLog : IModel
    {
        #region 字段

        private int fundsLogId;
        private int contractId;
        private int subId;
        private int invoiceId;
        private DateTime logDate;
        private int inBlocId;
        private int inCorpId;
        private int inBankId;
        private int inAccountId;
        private int outBlocId;
        private int outCorpId;
        private int outBankId;
        private string outBank = String.Empty;
        private int outAccountId;
        private string outAccount = String.Empty;
        private decimal fundsBala;
        private int fundsType;
        private int currencyId;
        private int logDirection;
        private int logType;
        private int payMode;
        private bool isVirtualPay;
        private string fundsDesc = String.Empty;
        private int opPerson;
        private string logSourceBase = String.Empty;
        private string logSource = String.Empty;
        private int sourceId;
        private StatusEnum logStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Fun_FundsLog";
        #endregion

        #region 构造函数

        public FundsLog()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 资金流水序号
        /// </summary>
        public int FundsLogId
        {
            get { return fundsLogId; }
            set { fundsLogId = value; }
        }

        /// <summary>
        /// 合约序号
        /// </summary>
        public int ContractId
        {
            get { return contractId; }
            set { contractId = value; }
        }

        /// <summary>
        /// 子合约序号
        /// </summary>
        public int SubId
        {
            get { return subId; }
            set { subId = value; }
        }

        /// <summary>
        /// 发票序号
        /// </summary>
        public int InvoiceId
        {
            get { return invoiceId; }
            set { invoiceId = value; }
        }

        /// <summary>
        /// 流水日期
        /// </summary>
        public DateTime LogDate
        {
            get { return logDate; }
            set { logDate = value; }
        }

        /// <summary>
        /// 集团序号
        /// </summary>
        public int InBlocId
        {
            get { return inBlocId; }
            set { inBlocId = value; }
        }

        /// <summary>
        /// 公司序号
        /// </summary>
        public int InCorpId
        {
            get { return inCorpId; }
            set { inCorpId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int InBankId
        {
            get { return inBankId; }
            set { inBankId = value; }
        }

        /// <summary>
        /// 我方银行账户
        /// </summary>
        public int InAccountId
        {
            get { return inAccountId; }
            set { inAccountId = value; }
        }

        /// <summary>
        /// 对方集团
        /// </summary>
        public int OutBlocId
        {
            get { return outBlocId; }
            set { outBlocId = value; }
        }

        /// <summary>
        /// 对方公司
        /// </summary>
        public int OutCorpId
        {
            get { return outCorpId; }
            set { outCorpId = value; }
        }

        /// <summary>
        /// 对方银行序号
        /// </summary>
        public int OutBankId
        {
            get { return outBankId; }
            set { outBankId = value; }
        }

        /// <summary>
        /// 对方银行
        /// </summary>
        public string OutBank
        {
            get { return outBank; }
            set { outBank = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int OutAccountId
        {
            get { return outAccountId; }
            set { outAccountId = value; }
        }

        /// <summary>
        /// 对方银行账户
        /// </summary>
        public string OutAccount
        {
            get { return outAccount; }
            set { outAccount = value; }
        }

        /// <summary>
        /// 资金数量
        /// </summary>
        public decimal FundsBala
        {
            get { return fundsBala; }
            set { fundsBala = value; }
        }

        /// <summary>
        /// 资金类型
        /// </summary>
        public int FundsType
        {
            get { return fundsType; }
            set { fundsType = value; }
        }

        /// <summary>
        /// 币种
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }

        /// <summary>
        /// 流水方向
        /// </summary>
        public int LogDirection
        {
            get { return logDirection; }
            set { logDirection = value; }
        }

        /// <summary>
        /// 操作类型/收款，收款冲销，付款，付款冲销
        /// </summary>
        public int LogType
        {
            get { return logType; }
            set { logType = value; }
        }

        /// <summary>
        /// 付款方式
        /// </summary>
        public int PayMode
        {
            get { return payMode; }
            set { payMode = value; }
        }

        /// <summary>
        /// 是否虚拟付款
        /// </summary>
        public bool IsVirtualPay
        {
            get { return isVirtualPay; }
            set { isVirtualPay = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string FundsDesc
        {
            get { return fundsDesc; }
            set { fundsDesc = value; }
        }

        /// <summary>
        /// 操作人
        /// </summary>
        public int OpPerson
        {
            get { return opPerson; }
            set { opPerson = value; }
        }

        /// <summary>
        /// 流水来源库名
        /// </summary>
        public string LogSourceBase
        {
            get { return logSourceBase; }
            set { logSourceBase = value; }
        }

        /// <summary>
        /// 流水来源/表名记录
        /// </summary>
        public string LogSource
        {
            get { return logSource; }
            set { logSource = value; }
        }

        /// <summary>
        /// 来源编号/表序号记录
        /// </summary>
        public int SourceId
        {
            get { return sourceId; }
            set { sourceId = value; }
        }

        /// <summary>
        /// 流水状态
        /// </summary>
        public StatusEnum LogStatus
        {
            get { return logStatus; }
            set { logStatus = value; }
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
            get { return this.fundsLogId; }
            set { this.fundsLogId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return logStatus; }
            set { logStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Funds.DAL.FundsLogDAL";
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