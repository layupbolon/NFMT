
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoicePayApply.cs
// 文件功能描述：发票付款申请dbo.Fun_InvoicePayApply_Ref实体类。
// 创建人：CodeSmith
// 创建时间： 2014年10月27日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 发票付款申请dbo.Fun_InvoicePayApply_Ref实体类。
    /// </summary>
    [Serializable]
    public class InvoicePayApply : IModel
    {
        #region 字段

        private int refId;
        private int payApplyId;
        private int invoiceId;
        private int sIId;
        private decimal applyBala;
        private Common.StatusEnum detailStatus;
        private string tableName = "dbo.Fun_InvoicePayApply_Ref";
        #endregion

        #region 构造函数

        public InvoicePayApply()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 发票付款申请序号
        /// </summary>
        public int RefId
        {
            get { return refId; }
            set { refId = value; }
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
        /// 发票序号
        /// </summary>
        public int InvoiceId
        {
            get { return invoiceId; }
            set { invoiceId = value; }
        }

        /// <summary>
        /// 价外票序号
        /// </summary>
        public int SIId
        {
            get { return sIId; }
            set { sIId = value; }
        }

        /// <summary>
        /// 申请金额
        /// </summary>
        public decimal ApplyBala
        {
            get { return applyBala; }
            set { applyBala = value; }
        }

        /// <summary>
        /// 明细状态
        /// </summary>
        public Common.StatusEnum DetailStatus
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
            get { return this.refId; }
            set { this.refId = value; }
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

        private string dalName = "NFMT.Funds.DAL.InvoicePayApplyDAL";
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