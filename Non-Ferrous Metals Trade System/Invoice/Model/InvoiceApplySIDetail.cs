
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceApplySIDetail.cs
// 文件功能描述：价外票开票申请明细dbo.Inv_InvoiceApplySIDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2015年7月28日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Invoice.Model
{
    /// <summary>
    /// 价外票开票申请明细dbo.Inv_InvoiceApplySIDetail实体类。
    /// </summary>
    [Serializable]
    public class InvoiceApplySIDetail : IModel
    {
        #region 字段

        private int sIDetailId;
        private int invoiceApplyId;
        private int applyId;
        private int invoiceId;
        private int sIId;
        private Common.StatusEnum detailStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Inv_InvoiceApplySIDetail";
        #endregion

        #region 构造函数

        public InvoiceApplySIDetail()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 开票申请序号
        /// </summary>
        public int SIDetailId
        {
            get { return sIDetailId; }
            set { sIDetailId = value; }
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
        /// 申请序号
        /// </summary>
        public int ApplyId
        {
            get { return applyId; }
            set { applyId = value; }
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
        /// 业务发票序号
        /// </summary>
        public int SIId
        {
            get { return sIId; }
            set { sIId = value; }
        }

        /// <summary>
        /// 明细状态
        /// </summary>
        public Common.StatusEnum DetailStatus
        {
            get { return detailStatus; }
            set { detailStatus = value; }
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
            get { return this.sIDetailId; }
            set { this.sIDetailId = value; }
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

        private string dalName = "NFMT.Invoice.DAL.InvoiceApplySIDetailDAL";
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