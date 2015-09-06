
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentStockDetail.cs
// 文件功能描述：库存财务付款明细dbo.Fun_PaymentStockDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2015年3月5日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
	/// <summary>
	/// 库存财务付款明细dbo.Fun_PaymentStockDetail实体类。
	/// </summary>
	[Serializable]
	public class PaymentStockDetail : IModel
	{
		#region 字段
        
		private int detailId;
		private int contractDetailId;
		private int paymentId;
		private int stockId;
		private int stockLogId;
		private int contractId;
		private int subId;
		private int payApplyId;
		private int payApplyDetailId;
		private decimal payBala;
		private decimal fundsBala;
		private decimal virtualBala;
		private int sourceFrom;
        private Common.StatusEnum detailStatus;
        private string tableName = "dbo.Fun_PaymentStockDetail";
		#endregion
		
		#region 构造函数
        
		public PaymentStockDetail()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 序号
        /// </summary>
		public int DetailId
		{
			get {return detailId;}
			set {detailId = value;}
		}

        /// <summary>
        /// 合约付款序号
        /// </summary>
		public int ContractDetailId
		{
			get {return contractDetailId;}
			set {contractDetailId = value;}
		}

        /// <summary>
        /// 付款序号
        /// </summary>
		public int PaymentId
		{
			get {return paymentId;}
			set {paymentId = value;}
		}

        /// <summary>
        /// 库存序号
        /// </summary>
		public int StockId
		{
			get {return stockId;}
			set {stockId = value;}
		}

        /// <summary>
        /// 库存流水序号
        /// </summary>
		public int StockLogId
		{
			get {return stockLogId;}
			set {stockLogId = value;}
		}

        /// <summary>
        /// 合约序号
        /// </summary>
		public int ContractId
		{
			get {return contractId;}
			set {contractId = value;}
		}

        /// <summary>
        /// 子合约序号
        /// </summary>
		public int SubId
		{
			get {return subId;}
			set {subId = value;}
		}

        /// <summary>
        /// 付款申请序号
        /// </summary>
		public int PayApplyId
		{
			get {return payApplyId;}
			set {payApplyId = value;}
		}

        /// <summary>
        /// 合约付款申请明细序号
        /// </summary>
		public int PayApplyDetailId
		{
			get {return payApplyDetailId;}
			set {payApplyDetailId = value;}
		}

        /// <summary>
        /// 付款金额
        /// </summary>
		public decimal PayBala
		{
			get {return payBala;}
			set {payBala = value;}
		}

        /// <summary>
        /// 财务付款金额
        /// </summary>
		public decimal FundsBala
		{
			get {return fundsBala;}
			set {fundsBala = value;}
		}

        /// <summary>
        /// 虚拟付款金额
        /// </summary>
		public decimal VirtualBala
		{
			get {return virtualBala;}
			set {virtualBala = value;}
		}

        /// <summary>
        /// 数据来源
        /// </summary>
		public int SourceFrom
		{
			get {return sourceFrom;}
			set {sourceFrom = value;}
		}

        /// <summary>
        /// 明细状态
        /// </summary>
		public Common.StatusEnum DetailStatus
		{
			get {return detailStatus;}
			set {detailStatus = value;}
		}
        
        public int CreatorId{get;set;}
public DateTime CreateTime{get;set;}
public int LastModifyId{get;set;}
public DateTime LastModifyTime{get;set;}
        
        /// <summary>
        /// 数据序号
        /// </summary>
        public int Id
        {
            get { return this.detailId;}
            set { this.detailId = value;}
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
        
        private string dalName = "NFMT.Funds.DAL.PaymentStockDetailDAL";
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