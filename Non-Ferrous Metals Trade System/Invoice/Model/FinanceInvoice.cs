
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FinanceInvoice.cs
// 文件功能描述：财务发票dbo.Inv_FinanceInvoice实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月25日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Invoice.Model
{
	/// <summary>
	/// 财务发票dbo.Inv_FinanceInvoice实体类。
	/// </summary>
	[Serializable]
	public class FinanceInvoice : IModel
	{
		#region 字段
        
		private int financeInvoiceId;
		private int invoiceId;
		private int assetId;
		private decimal integerAmount;
		private decimal netAmount;
		private int mUId;
		private decimal vATRatio;
		private decimal vATBala;
        private string tableName = "dbo.Inv_FinanceInvoice";
		#endregion
		
		#region 构造函数
        
		public FinanceInvoice()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 财务发票序号
        /// </summary>
		public int FinanceInvoiceId
		{
			get {return financeInvoiceId;}
			set {financeInvoiceId = value;}
		}

        /// <summary>
        /// 主表发票序号
        /// </summary>
		public int InvoiceId
		{
			get {return invoiceId;}
			set {invoiceId = value;}
		}

        /// <summary>
        /// 品种
        /// </summary>
		public int AssetId
		{
			get {return assetId;}
			set {assetId = value;}
		}

        /// <summary>
        /// 毛量
        /// </summary>
		public decimal IntegerAmount
		{
			get {return integerAmount;}
			set {integerAmount = value;}
		}

        /// <summary>
        /// 净量
        /// </summary>
		public decimal NetAmount
		{
			get {return netAmount;}
			set {netAmount = value;}
		}

        /// <summary>
        /// 计量单位
        /// </summary>
		public int MUId
		{
			get {return mUId;}
			set {mUId = value;}
		}

        /// <summary>
        /// 增值税率
        /// </summary>
		public decimal VATRatio
		{
			get {return vATRatio;}
			set {vATRatio = value;}
		}

        /// <summary>
        /// 增值税
        /// </summary>
		public decimal VATBala
		{
			get {return vATBala;}
			set {vATBala = value;}
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
            get { return this.financeInvoiceId;}
            set { this.financeInvoiceId = value;}
        }
        
        
        
        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get;set;
        }
        
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        
        private string dalName = "NFMT.Funds.DAL.FinanceInvoiceDAL";
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