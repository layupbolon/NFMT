
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StopLossApply.cs
// 文件功能描述：止损申请dbo.Pri_StopLossApply实体类。
// 创建人：CodeSmith
// 创建时间： 2014年10月23日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
	/// <summary>
	/// 止损申请dbo.Pri_StopLossApply实体类。
	/// </summary>
	[Serializable]
	public class StopLossApply : IModel
	{
		#region 字段
        
		private int stopLossApplyId;
		private int applyId;
		private int pricingId;
		private int pricingDirection;
		private int subContractId;
		private int contractId;
		private int assertId;
		private decimal stopLossPrice;
		private int currencyId;
		private decimal stopLossWeight;
		private int mUId;
        private string tableName = "dbo.Pri_StopLossApply";
		#endregion
		
		#region 构造函数
        
		public StopLossApply()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 点价申请序号
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
        /// 点价序号
        /// </summary>
		public int PricingId
		{
			get {return pricingId;}
			set {pricingId = value;}
		}

        /// <summary>
        /// 点价方向
        /// </summary>
		public int PricingDirection
		{
			get {return pricingDirection;}
			set {pricingDirection = value;}
		}

        /// <summary>
        /// 子合约序号
        /// </summary>
		public int SubContractId
		{
			get {return subContractId;}
			set {subContractId = value;}
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
        /// 止损品种
        /// </summary>
		public int AssertId
		{
			get {return assertId;}
			set {assertId = value;}
		}

        /// <summary>
        /// 止损价格
        /// </summary>
		public decimal StopLossPrice
		{
			get {return stopLossPrice;}
			set {stopLossPrice = value;}
		}

        /// <summary>
        /// 价格币种
        /// </summary>
		public int CurrencyId
		{
			get {return currencyId;}
			set {currencyId = value;}
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
        /// 重量单位
        /// </summary>
		public int MUId
		{
			get {return mUId;}
			set {mUId = value;}
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
            get { return this.stopLossApplyId;}
            set { this.stopLossApplyId = value;}
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
        
        private string dalName = "NFMT.DoPrice.DAL.StopLossApplyDAL";
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