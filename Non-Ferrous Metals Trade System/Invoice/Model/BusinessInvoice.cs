
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BusinessInvoice.cs
// 文件功能描述：业务发票dbo.Inv_BusinessInvoice实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月10日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Invoice.Model
{
    /// <summary>
    /// 业务发票dbo.Inv_BusinessInvoice实体类。
    /// </summary>
    [Serializable]
    public class BusinessInvoice : IModel
    {
        #region 字段

        private int businessInvoiceId;
        private int invoiceId;
        private int refInvoiceId;
        private int contractId;
        private int subContractId;
        private int assetId;
        private decimal integerAmount;
        private decimal netAmount;
        private decimal unitPrice;
        private int mUId;
        private decimal marginRatio;
        private decimal vATRatio;
        private decimal vATBala;
        private string tableName = "dbo.Inv_BusinessInvoice";
        #endregion

        #region 构造函数

        public BusinessInvoice()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 业务发票序号
        /// </summary>
        public int BusinessInvoiceId
        {
            get { return businessInvoiceId; }
            set { businessInvoiceId = value; }
        }

        /// <summary>
        /// 主表发票序号
        /// </summary>
        public int InvoiceId
        {
            get { return invoiceId; }
            set { invoiceId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int RefInvoiceId
        {
            get { return refInvoiceId; }
            set { refInvoiceId = value; }
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
        public int SubContractId
        {
            get { return subContractId; }
            set { subContractId = value; }
        }

        /// <summary>
        /// 品种
        /// </summary>
        public int AssetId
        {
            get { return assetId; }
            set { assetId = value; }
        }

        /// <summary>
        /// 毛量
        /// </summary>
        public decimal IntegerAmount
        {
            get { return integerAmount; }
            set { integerAmount = value; }
        }

        /// <summary>
        /// 净量
        /// </summary>
        public decimal NetAmount
        {
            get { return netAmount; }
            set { netAmount = value; }
        }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        /// <summary>
        /// 计量单位
        /// </summary>
        public int MUId
        {
            get { return mUId; }
            set { mUId = value; }
        }

        /// <summary>
        /// 保证金比例
        /// </summary>
        public decimal MarginRatio
        {
            get { return marginRatio; }
            set { marginRatio = value; }
        }

        /// <summary>
        /// 增值税率
        /// </summary>
        public decimal VATRatio
        {
            get { return vATRatio; }
            set { vATRatio = value; }
        }

        /// <summary>
        /// 增值税
        /// </summary>
        public decimal VATBala
        {
            get { return vATBala; }
            set { vATBala = value; }
        }

        public int CreatorId { get; set; }
        public DateTime CreateTime { get; set; }
        public int LastModifyId { get; set; }
        public DateTime LastModifyTime { get; set; }

        /// <summary>
        /// 数据序号
        /// </summary>
        public int Id
        {
            get { return this.businessInvoiceId; }
            set { this.businessInvoiceId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get;
            set;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Invoice.DAL.BusinessInvoiceDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Invoice";
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