
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Rate.cs
// 文件功能描述：汇率dbo.Rate实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
	/// <summary>
	/// 汇率dbo.Rate实体类。
	/// </summary>
	[Serializable]
	public class Rate : IModel
	{
		#region 字段
        
		private int rateId;
		private int fromCurrencyId;
		private int toCurrencyId;
		private decimal rateValue;
        private Common.StatusEnum rateStatus;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.Rate";
        
		#endregion
		
		#region 构造函数
        
		public Rate()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 汇率序号
        /// </summary>
		public int RateId
		{
			get {return rateId;}
			set {rateId = value;}
		}

        /// <summary>
        /// 兑换币种序号
        /// </summary>
		public int FromCurrencyId
		{
			get {return fromCurrencyId;}
			set {fromCurrencyId = value;}
		}

        /// <summary>
        /// 换至币种序号
        /// </summary>
		public int ToCurrencyId
		{
			get {return toCurrencyId;}
			set {toCurrencyId = value;}
		}
        /// <summary>
        /// 兑换币种
        /// </summary>
        public string CurrencyName1 { get; set; }
        /// <summary>
        /// 换至币种
        /// </summary>
        public string CurrencyName2 { get; set; }
        
        /// <summary>
        /// 汇率
        /// </summary>
		public decimal RateValue
		{
			get {return rateValue;}
			set {rateValue = value;}
		}

        /// <summary>
        /// 汇率状态
        /// </summary>
        public Common.StatusEnum RateStatus
        {
            get{return rateStatus;}
            set{rateStatus = value;}
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
            get { return this.rateId;}
            set { this.rateId = value;}
        }
        
        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get{return rateStatus;}
            set{rateStatus = value;}
        }
        /// <summary>
        /// 数据状态名
        /// </summary>
        public string RateStatusName
        {
            get { return this.rateStatus.ToString(); }
        }
        
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.RateDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Data";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT_Basic";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
		#endregion
	}
}