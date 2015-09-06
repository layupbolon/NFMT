
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Currency.cs
// 文件功能描述：币种表dbo.Currency实体类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
	/// <summary>
	/// 币种表dbo.Currency实体类。
	/// </summary>
	[Serializable]
	public class Currency : IModel
	{
		#region 字段
        
		private int currencyId;
		private string currencyName = String.Empty;
        private Common.StatusEnum currencyStatus;
		private string currencyFullName = String.Empty;
		private string curencyShort = String.Empty;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.Currency";
        
		#endregion
		
		#region 构造函数
        
		public Currency()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 
        /// </summary>
		public int CurrencyId
		{
			get {return currencyId;}
			set {currencyId = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public string CurrencyName
		{
			get {return currencyName;}
			set {currencyName = value;}
		}

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum CurrencyStatus
        {
            get{return currencyStatus;}
            set{currencyStatus = value;}
        }
        /// <summary>
        /// 
        /// </summary>
		public string CurrencyFullName
		{
			get {return currencyFullName;}
			set {currencyFullName = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public string CurencyShort
		{
			get {return curencyShort;}
			set {curencyShort = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public int CreatorId
		{
			get {return creatorId;}
			set {creatorId = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public DateTime CreateTime
		{
			get {return createTime;}
			set {createTime = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public int LastModifyId
		{
			get {return lastModifyId;}
			set {lastModifyId = value;}
		}

        /// <summary>
        /// 
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
            get { return this.currencyId;}
            set { this.currencyId = value;}
        }
        
        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get{return currencyStatus;}
            set{currencyStatus = value;}
        }
        /// <summary>
        /// 数据状态名
        /// </summary>
        public string CurrencyStatusName
        {
            get { return this.currencyStatus.ToString(); }
        }
        
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.CurrencyDAL";
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