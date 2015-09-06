
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentVirtual.cs
// 文件功能描述：虚拟收付款dbo.Fun_PaymentVirtual实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月24日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 虚拟收付款dbo.Fun_PaymentVirtual实体类。
    /// </summary>
    [Serializable]
    public class PaymentVirtual : IModel
    {
        #region 字段

        private int virtualId;
        private int paymentId;
        private int payApplyId;
        private decimal payBala;
        private StatusEnum detailStatus;
        private bool isConfirm;
        private string memo = String.Empty;
        private string confirmMemo = String.Empty;
        private int fundsLogId;
        private string tableName = "dbo.Fun_PaymentVirtual";
        #endregion

        #region 构造函数

        public PaymentVirtual()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 虚拟收款序号
        /// </summary>
        public int VirtualId
        {
            get { return virtualId; }
            set { virtualId = value; }
        }

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
        /// 付款金额
        /// </summary>
        public decimal PayBala
        {
            get { return payBala; }
            set { payBala = value; }
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
        /// 是否确认
        /// </summary>
        public bool IsConfirm
        {
            get { return isConfirm; }
            set { isConfirm = value; }
        }

        /// <summary>
        /// 虚拟收付款备注
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        /// <summary>
        /// 虚拟收付款确认备注
        /// </summary>
        public string ConfirmMemo
        {
            get { return confirmMemo; }
            set { confirmMemo = value; }
        }

        /// <summary>
        /// 资金流水序号
        /// </summary>
        public int FundsLogId
        {
            get { return fundsLogId; }
            set { fundsLogId = value; }
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
            get { return this.virtualId; }
            set { this.virtualId = value; }
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

        private string dalName = "NFMT.Funds.DAL.PaymentVirtualDAL";
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