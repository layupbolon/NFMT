
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SubDetail.cs
// 文件功能描述：子合约明细dbo.Con_SubDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月28日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Contract.Model
{
	/// <summary>
	/// 子合约明细dbo.Con_SubDetail实体类。
	/// </summary>
	[Serializable]
	public class SubDetail : IModel
	{
		#region 字段
        
		private int subDetailId;
		private int subId;
		private int discountBase;
		private int discountType;
		private decimal discountRate;
		private int delayType;
		private decimal delayRate;
		private decimal moreOrLess;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.Con_SubDetail";
		#endregion
		
		#region 构造函数
        
		public SubDetail()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 子合约明细序号
        /// </summary>
		public int SubDetailId
		{
			get {return subDetailId;}
			set {subDetailId = value;}
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
        /// 贴现利率基准
        /// </summary>
		public int DiscountBase
		{
			get {return discountBase;}
			set {discountBase = value;}
		}

        /// <summary>
        /// 贴现规则(按比例/金额)
        /// </summary>
		public int DiscountType
		{
			get {return discountType;}
			set {discountType = value;}
		}

        /// <summary>
        /// 贴现利率
        /// </summary>
		public decimal DiscountRate
		{
			get {return discountRate;}
			set {discountRate = value;}
		}

        /// <summary>
        /// 延期规则(按比例/金额)
        /// </summary>
		public int DelayType
		{
			get {return delayType;}
			set {delayType = value;}
		}

        /// <summary>
        /// 延期费/率
        /// </summary>
		public decimal DelayRate
		{
			get {return delayRate;}
			set {delayRate = value;}
		}

        /// <summary>
        /// 溢短装
        /// </summary>
		public decimal MoreOrLess
		{
			get {return moreOrLess;}
			set {moreOrLess = value;}
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
            get { return this.subDetailId;}
            set { this.subDetailId = value;}
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
        
        private string dalName = "NFMT.Contract.DAL.SubDetailDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }
        
        private string assName = "NFMT.Contract";
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
            get { return dataBaseName; }
        }

		#endregion
	}
}