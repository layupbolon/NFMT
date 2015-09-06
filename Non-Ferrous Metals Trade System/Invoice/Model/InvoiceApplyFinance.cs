
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceApplyFinance.cs
// 文件功能描述：开票申请与财务票关联表dbo.Inv_InvoiceApplyFinance实体类。
// 创建人：CodeSmith
// 创建时间： 2015年7月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Invoice.Model
{
    /// <summary>
    /// 开票申请与财务票关联表dbo.Inv_InvoiceApplyFinance实体类。
    /// </summary>
    [Serializable]
    public class InvoiceApplyFinance : IModel
    {
        #region 字段

        private int refId;
        private int invoiceId;
        private int financeInvoiceId;
        private int invoiceApplyId;
        private Common.StatusEnum refStatus;
        private string tableName = "dbo.Inv_InvoiceApplyFinance";
        #endregion

        #region 构造函数

        public InvoiceApplyFinance()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public int RefId
        {
            get { return refId; }
            set { refId = value; }
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
        /// 财务发票序号
        /// </summary>
        public int FinanceInvoiceId
        {
            get { return financeInvoiceId; }
            set { financeInvoiceId = value; }
        }

        /// <summary>
        /// 开票申请序号
        /// </summary>
        public int InvoiceApplyId
        {
            get { return invoiceApplyId; }
            set { invoiceApplyId = value; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public Common.StatusEnum RefStatus
        {
            get { return refStatus; }
            set { refStatus = value; }
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
            get { return this.refId; }
            set { this.refId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return refStatus; }
            set { refStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Invoice.DAL.InvoiceApplyFinanceDAL";
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