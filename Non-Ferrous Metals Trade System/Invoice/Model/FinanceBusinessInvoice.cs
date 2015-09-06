
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FinanceBusinessInvoice.cs
// 文件功能描述：业务发票财务发票关联表dbo.Inv_FinanceBusinessInvoice_Ref实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月25日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Invoice.Model
{
	/// <summary>
	/// 业务发票财务发票关联表dbo.Inv_FinanceBusinessInvoice_Ref实体类。
	/// </summary>
	[Serializable]
	public class FinanceBusinessInvoice : IModel
	{
		#region 字段
        
		private int refId;
		private int businessInvoiceId;
		private int financeInvoiceId;
		private decimal bala;
        private string tableName = "dbo.Inv_FinanceBusinessInvoice_Ref";
		#endregion
		
		#region 构造函数
        
		public FinanceBusinessInvoice()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 关联表序号
        /// </summary>
		public int RefId
		{
			get {return refId;}
			set {refId = value;}
		}

        /// <summary>
        /// 业务发票序号
        /// </summary>
		public int BusinessInvoiceId
		{
			get {return businessInvoiceId;}
			set {businessInvoiceId = value;}
		}

        /// <summary>
        /// 财务发票序号
        /// </summary>
		public int FinanceInvoiceId
		{
			get {return financeInvoiceId;}
			set {financeInvoiceId = value;}
		}

        /// <summary>
        /// 金额
        /// </summary>
		public decimal Bala
		{
			get {return bala;}
			set {bala = value;}
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
            get { return this.refId;}
            set { this.refId = value;}
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
        
        private string dalName = "NFMT.Funds.DAL.FinanceBusinessInvoiceDAL";
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