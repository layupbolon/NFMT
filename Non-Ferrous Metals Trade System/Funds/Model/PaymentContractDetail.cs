
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentContractDetail.cs
// 文件功能描述：合约财务付款明细dbo.Fun_PaymentContractDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月23日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 合约财务付款明细dbo.Fun_PaymentContractDetail实体类。
    /// </summary>
    [Serializable]
    public class PaymentContractDetail : IModel
    {
        #region 字段

        private int detailId;
        private int paymentId;
        private int contractId;
        private int contractSubId;
        private int payApplyId;
        private int payApplyDetailId;
        private decimal payBala;
        private decimal fundsBala;
        private decimal virtualBala;
        private string tableName = "dbo.Fun_PaymentContractDetail";
        #endregion

        #region 构造函数

        public PaymentContractDetail()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public int DetailId
        {
            get { return detailId; }
            set { detailId = value; }
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
        /// 
        /// </summary>
        public int ContractId
        {
            get { return contractId; }
            set { contractId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ContractSubId
        {
            get { return contractSubId; }
            set { contractSubId = value; }
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
        /// 合约付款申请明细序号
        /// </summary>
        public int PayApplyDetailId
        {
            get { return payApplyDetailId; }
            set { payApplyDetailId = value; }
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
        /// 财务付款金额
        /// </summary>
        public decimal FundsBala
        {
            get { return fundsBala; }
            set { fundsBala = value; }
        }

        /// <summary>
        /// 虚拟付款金额
        /// </summary>
        public decimal VirtualBala
        {
            get { return virtualBala; }
            set { virtualBala = value; }
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

        private string dalName = "NFMT.Funds.DAL.PaymentContractDetailDAL";
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