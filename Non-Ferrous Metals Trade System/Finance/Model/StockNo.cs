
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockNo.cs
// 文件功能描述：融资单号表dbo.Fin_StockNo实体类。
// 创建人：CodeSmith
// 创建时间： 2015年5月14日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Finance.Model
{
	/// <summary>
	/// 融资单号表dbo.Fin_StockNo实体类。
	/// </summary>
	[Serializable]
	public class StockNo : IModel
	{
		#region 字段
        
		private int stockId;
		private string refNo = String.Empty;
		private decimal netAmount;
        private string tableName = "dbo.Fin_StockNo";
		#endregion
		
		#region 构造函数
        
		public StockNo()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 序号
        /// </summary>
		public int StockId
		{
			get {return stockId;}
			set {stockId = value;}
		}

        /// <summary>
        /// 业务单号
        /// </summary>
		public string RefNo
		{
			get {return refNo;}
			set {refNo = value;}
		}

        /// <summary>
        /// 净重
        /// </summary>
		public decimal NetAmount
		{
			get {return netAmount;}
			set {netAmount = value;}
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
            get { return this.stockId;}
            set { this.stockId = value;}
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
        
        private string dalName = "NFMT.Finance.DAL.StockNoDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }
        
        private string assName = "NFMT.Finance";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }        
        }
        
        public string StatusName
        {
            get { return this.Status.ToString(); }
        }
        
        private string dataBaseName = "Financing";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
		#endregion
	}
}