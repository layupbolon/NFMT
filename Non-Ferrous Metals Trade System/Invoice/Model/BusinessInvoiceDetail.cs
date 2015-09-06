
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BusinessInvoiceDetail.cs
// 文件功能描述：业务发票明细dbo.Inv_BusinessInvoiceDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2015年3月26日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Invoice.Model
{
    /// <summary>
    /// 业务发票明细dbo.Inv_BusinessInvoiceDetail实体类。
    /// </summary>
    [Serializable]
    public class BusinessInvoiceDetail : IModel
    {
        #region 字段

        private int detailId;
        private int businessInvoiceId;
        private int invoiceId;
        private int refDetailId;
        private int stockId;
        private int stockLogId;
        private int confirmPriceId;
        private int confirmDetailId;
        private int pricingId;
        private int pricingDetailId;
        private int feeType;
        private decimal integerAmount;
        private decimal netAmount;
        private decimal unitPrice;
        private decimal calculateDay;
        private decimal bala;
        private StatusEnum detailStatus;
        private string tableName = "dbo.Inv_BusinessInvoiceDetail";
        #endregion

        #region 构造函数

        public BusinessInvoiceDetail()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 明细序号
        /// </summary>
        public int DetailId
        {
            get { return detailId; }
            set { detailId = value; }
        }

        /// <summary>
        /// 业务发票序号
        /// </summary>
        public int BusinessInvoiceId
        {
            get { return businessInvoiceId; }
            set { businessInvoiceId = value; }
        }

        /// <summary>
        /// 主表发票序号.
        /// </summary>
        public int InvoiceId
        {
            get { return invoiceId; }
            set { invoiceId = value; }
        }

        /// <summary>
        /// 关联明细序号
        /// </summary>
        public int RefDetailId
        {
            get { return refDetailId; }
            set { refDetailId = value; }
        }

        /// <summary>
        /// 库存序号
        /// </summary>
        public int StockId
        {
            get { return stockId; }
            set { stockId = value; }
        }

        /// <summary>
        /// 库存流水序号
        /// </summary>
        public int StockLogId
        {
            get { return stockLogId; }
            set { stockLogId = value; }
        }

        /// <summary>
        /// 价格确认序号
        /// </summary>
        public int ConfirmPriceId
        {
            get { return confirmPriceId; }
            set { confirmPriceId = value; }
        }

        /// <summary>
        /// 价格确认明细序号
        /// </summary>
        public int ConfirmDetailId
        {
            get { return confirmDetailId; }
            set { confirmDetailId = value; }
        }

        /// <summary>
        /// 点价序号
        /// </summary>
        public int PricingId
        {
            get { return pricingId; }
            set { pricingId = value; }
        }

        /// <summary>
        /// 点价明细序号
        /// </summary>
        public int PricingDetailId
        {
            get { return pricingDetailId; }
            set { pricingDetailId = value; }
        }

        /// <summary>
        /// 发票内容
        /// </summary>
        public int FeeType
        {
            get { return feeType; }
            set { feeType = value; }
        }

        /// <summary>
        /// 毛吨
        /// </summary>
        public decimal IntegerAmount
        {
            get { return integerAmount; }
            set { integerAmount = value; }
        }

        /// <summary>
        /// 净吨
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
        /// 计价天数
        /// </summary>
        public decimal CalculateDay
        {
            get { return calculateDay; }
            set { calculateDay = value; }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Bala
        {
            get { return bala; }
            set { bala = value; }
        }

        /// <summary>
        /// 明细状态
        /// </summary>
        public StatusEnum DetailStatus
        {
            get { return detailStatus; }
            set { detailStatus = value; }
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
            get { return this.detailId; }
            set { this.detailId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return detailStatus; }
            set { detailStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Invoice.DAL.BusinessInvoiceDetailDAL";
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