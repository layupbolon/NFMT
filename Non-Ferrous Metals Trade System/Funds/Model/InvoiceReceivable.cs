
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceReceivable.cs
// 文件功能描述：收款分配至发票dbo.Fun_InvoiceReceivable_Ref实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
	/// <summary>
	/// 收款分配至发票dbo.Fun_InvoiceReceivable_Ref实体类。
	/// </summary>
	[Serializable]
	public class InvoiceReceivable : IModel
	{
		#region 字段
        
		private int refId;
		private int receId;
		private int invoiceId;
		private int detailId;
        private string tableName = "dbo.Fun_InvoiceReceivable_Ref";
		#endregion
		
		#region 构造函数
        
		public InvoiceReceivable()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 序号
        /// </summary>
		public int RefId
		{
			get {return refId;}
			set {refId = value;}
		}

        /// <summary>
        /// 收款登记序号
        /// </summary>
		public int ReceId
		{
			get {return receId;}
			set {receId = value;}
		}

        /// <summary>
        /// 发票序号
        /// </summary>
		public int InvoiceId
		{
			get {return invoiceId;}
			set {invoiceId = value;}
		}

        /// <summary>
        /// 明细序号
        /// </summary>
		public int DetailId
		{
			get {return detailId;}
			set {detailId = value;}
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
        
        private string dalName = "NFMT.Funds.DAL.InvoiceReceivableDAL";
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