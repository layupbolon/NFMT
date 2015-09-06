
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentInvoice.cs
// 文件功能描述：制单发票明细dbo.Doc_DocumentInvoice实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Document.Model
{
    /// <summary>
    /// 制单发票明细dbo.Doc_DocumentInvoice实体类。
    /// </summary>
    [Serializable]
    public class DocumentInvoice : IModel
    {
        #region 字段

        private int detailId;
        private int documentId;
        private int orderId;
        private int stockDetailId;
        private int orderInvoiceDetailId;
        private string invoiceNo = String.Empty;
        private int invoiceId;
        private decimal invoiceBala;
        private StatusEnum detailStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Doc_DocumentInvoice";
        #endregion

        #region 构造函数

        public DocumentInvoice()
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
        /// 制单序号
        /// </summary>
        public int DocumentId
        {
            get { return documentId; }
            set { documentId = value; }
        }

        /// <summary>
        /// 指令序号
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        /// <summary>
        /// 库存明细序号
        /// </summary>
        public int StockDetailId
        {
            get { return stockDetailId; }
            set { stockDetailId = value; }
        }

        /// <summary>
        /// 指令发票明细序号
        /// </summary>
        public int OrderInvoiceDetailId
        {
            get { return orderInvoiceDetailId; }
            set { orderInvoiceDetailId = value; }
        }

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
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
        /// 发票金额
        /// </summary>
        public decimal InvoiceBala
        {
            get { return invoiceBala; }
            set { invoiceBala = value; }
        }

        /// <summary>
        /// 明细状态
        /// </summary>
        public StatusEnum DetailStatus
        {
            get { return detailStatus; }
            set { detailStatus = value; }
        }

        /// <summary>
        /// 创建人
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

        private string dalName = "NFMT.Document.DAL.DocumentInvoiceDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Document";
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