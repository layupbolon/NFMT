
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StopLossApplyDetail.cs
// 文件功能描述：止损申请明细dbo.Pri_StopLossApplyDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年10月23日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
	/// <summary>
	/// 止损申请明细dbo.Pri_StopLossApplyDetail实体类。
	/// </summary>
	[Serializable]
	public class StopLossApplyDetail : IModel
	{
		#region 字段
        
		private int detailId;
		private int stopLossApplyId;
		private int applyId;
		private int pricingDetailId;
		private int stockId;
		private int stockLogId;
		private decimal stopLossWeight;
        private Common.StatusEnum detailStatus;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.Pri_StopLossApplyDetail";
		#endregion
		
		#region 构造函数
        
		public StopLossApplyDetail()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 明细序号
        /// </summary>
		public int DetailId
		{
			get {return detailId;}
			set {detailId = value;}
		}

        /// <summary>
        /// 止损申请序号
        /// </summary>
		public int StopLossApplyId
		{
			get {return stopLossApplyId;}
			set {stopLossApplyId = value;}
		}

        /// <summary>
        /// 申请主表序号
        /// </summary>
		public int ApplyId
		{
			get {return applyId;}
			set {applyId = value;}
		}

        /// <summary>
        /// 点价明细表
        /// </summary>
		public int PricingDetailId
		{
			get {return pricingDetailId;}
			set {pricingDetailId = value;}
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
        /// 止损重量
        /// </summary>
		public decimal StopLossWeight
		{
			get {return stopLossWeight;}
			set {stopLossWeight = value;}
		}

        /// <summary>
        /// 明细状态
        /// </summary>
		public Common.StatusEnum DetailStatus
		{
			get {return detailStatus;}
			set {detailStatus = value;}
		}

        /// <summary>
        /// 创建人
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
        /// 最后修改人
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
        
        private string dalName = "NFMT.DoPrice.DAL.StopLossApplyDetailDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }
        
        private string assName = "NFMT.DoPrice";
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