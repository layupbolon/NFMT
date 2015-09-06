
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentOrderDetail.cs
// 文件功能描述：制单指令明细dbo.Doc_DocumentOrderDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Document.Model
{
    /// <summary>
    /// 制单指令明细dbo.Doc_DocumentOrderDetail实体类。
    /// </summary>
    [Serializable]
    public class DocumentOrderDetail : IModel
    {
        #region 字段

        private int detailId;
        private int orderId;
        private int invoiceCopies;
        private string invoiceSpecific = String.Empty;
        private int qualityCopies;
        private string qualitySpecific = String.Empty;
        private int weightCopies;
        private string weightSpecific = String.Empty;
        private int texCopies;
        private string texSpecific = String.Empty;
        private int deliverCopies;
        private string deliverSpecific = String.Empty;
        private int totalInvCopies;
        private string totalInvSpecific = String.Empty;
        private StatusEnum detailStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Doc_DocumentOrderDetail";
        #endregion

        #region 构造函数

        public DocumentOrderDetail()
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
        /// 指令序号
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        /// <summary>
        /// 发票份数
        /// </summary>
        public int InvoiceCopies
        {
            get { return invoiceCopies; }
            set { invoiceCopies = value; }
        }

        /// <summary>
        /// 发票特殊要求
        /// </summary>
        public string InvoiceSpecific
        {
            get { return invoiceSpecific; }
            set { invoiceSpecific = value; }
        }

        /// <summary>
        /// 质量证数份数
        /// </summary>
        public int QualityCopies
        {
            get { return qualityCopies; }
            set { qualityCopies = value; }
        }

        /// <summary>
        /// 质量证书特殊要求
        /// </summary>
        public string QualitySpecific
        {
            get { return qualitySpecific; }
            set { qualitySpecific = value; }
        }

        /// <summary>
        /// 重量证份数
        /// </summary>
        public int WeightCopies
        {
            get { return weightCopies; }
            set { weightCopies = value; }
        }

        /// <summary>
        /// 重量证特殊要求
        /// </summary>
        public string WeightSpecific
        {
            get { return weightSpecific; }
            set { weightSpecific = value; }
        }

        /// <summary>
        /// 装箱单份数
        /// </summary>
        public int TexCopies
        {
            get { return texCopies; }
            set { texCopies = value; }
        }

        /// <summary>
        /// 装箱单特殊要求
        /// </summary>
        public string TexSpecific
        {
            get { return texSpecific; }
            set { texSpecific = value; }
        }

        /// <summary>
        /// 产地证明份数
        /// </summary>
        public int DeliverCopies
        {
            get { return deliverCopies; }
            set { deliverCopies = value; }
        }

        /// <summary>
        /// 产地证明特殊要求
        /// </summary>
        public string DeliverSpecific
        {
            get { return deliverSpecific; }
            set { deliverSpecific = value; }
        }

        /// <summary>
        /// 汇票份数
        /// </summary>
        public int TotalInvCopies
        {
            get { return totalInvCopies; }
            set { totalInvCopies = value; }
        }

        /// <summary>
        /// 汇票特殊要求
        /// </summary>
        public string TotalInvSpecific
        {
            get { return totalInvSpecific; }
            set { totalInvSpecific = value; }
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

        private string dalName = "NFMT.Document.DAL.DocumentOrderDetailDAL";
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