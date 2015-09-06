                                                                                                                                                                                                                                                                                                                              
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractFundsAllotStock.cs
// 文件功能描述：合约款分配至库存dbo.Fun_ContractFundsAllotStock_Ref实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
	/// <summary>
	/// 合约款分配至库存dbo.Fun_ContractFundsAllotStock_Ref实体类。
	/// </summary>
	[Serializable]
	public class ContractFundsAllotStock : IModel
	{
		#region 字段
        
		private int refId;
		private int allotId;
		private int receId;
		private int stockId;
		private decimal allotBala;
		private int currencyId;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.Fun_ContractFundsAllotStock_Ref";
		#endregion
		
		#region 构造函数
        
		public ContractFundsAllotStock()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 关联序号
        /// </summary>
		public int RefId
		{
			get {return refId;}
			set {refId = value;}
		}

        /// <summary>
        /// 分配序号
        /// </summary>
		public int AllotId
		{
			get {return allotId;}
			set {allotId = value;}
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
        /// 库存序号
        /// </summary>
		public int StockId
		{
			get {return stockId;}
			set {stockId = value;}
		}

        /// <summary>
        /// 金额
        /// </summary>
		public decimal AllotBala
		{
			get {return allotBala;}
			set {allotBala = value;}
		}

        /// <summary>
        /// 币种
        /// </summary>
		public int CurrencyId
		{
			get {return currencyId;}
			set {currencyId = value;}
		}

        /// <summary>
        /// 创建人序号
        /// </summary>
		public int CreatorId
		{
			get {return creatorId;}
			set {creatorId = value;}
		}

        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime
		{
			get {return createTime;}
			set {createTime = value;}
		}

        /// <summary>
        /// 最后修改人序号
        /// </summary>
		public int LastModifyId
		{
			get {return lastModifyId;}
			set {lastModifyId = value;}
		}

        /// <summary>
        /// 最后修改时间
        /// </summary>
		public DateTime LastModifyTime
		{
			get {return lastModifyTime;}
			set {lastModifyTime = value;}
		}
        
        
        
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
        
        private string dalName = "NFMT.Funds.DAL.ContractFundsAllotStockDAL";
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