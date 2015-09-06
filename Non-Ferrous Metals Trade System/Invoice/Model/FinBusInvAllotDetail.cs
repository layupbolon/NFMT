
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FinBusInvAllotDetail.cs
// 文件功能描述：业务发票财务发票分配明细dbo.Inv_FinBusInvAllotDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月25日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Invoice.Model
{
    /// <summary>
    /// 业务发票财务发票分配明细dbo.Inv_FinBusInvAllotDetail实体类。
    /// </summary>
    [Serializable]
    public class FinBusInvAllotDetail : IModel
    {
        #region 字段

        private int detailId;
        private int allotId;
        private int businessInvoiceId;
        private int financeInvoiceId;
        private decimal allotBala;
        private Common.StatusEnum detailStatus;
        private string tableName = "dbo.Inv_FinBusInvAllotDetail";
        #endregion

        #region 构造函数

        public FinBusInvAllotDetail()
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
        /// 分配序号
        /// </summary>
        public int AllotId
        {
            get { return allotId; }
            set { allotId = value; }
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
        /// 财务发票序号
        /// </summary>
        public int FinanceInvoiceId
        {
            get { return financeInvoiceId; }
            set { financeInvoiceId = value; }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal AllotBala
        {
            get { return allotBala; }
            set { allotBala = value; }
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

        private string dalName = "NFMT.Invoice.DAL.FinBusInvAllotDetailDAL";
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