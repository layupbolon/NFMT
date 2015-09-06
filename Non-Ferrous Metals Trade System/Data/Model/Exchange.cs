
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Exchange.cs
// 文件功能描述：dbo.Exchange实体类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
	/// <summary>
	/// dbo.Exchange实体类。
	/// </summary>
	[Serializable]
	public class Exchange : IModel
	{
		#region 字段
        
		private int exchangeId;
		private string exchangeName = String.Empty;
		private string exchangeCode = String.Empty;
        private Common.StatusEnum exchangeStatus;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.Exchange";
        
		#endregion
		
		#region 构造函数
        
		public Exchange()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 
        /// </summary>
		public int ExchangeId
		{
			get {return exchangeId;}
			set {exchangeId = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public string ExchangeName
		{
			get {return exchangeName;}
			set {exchangeName = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public string ExchangeCode
		{
			get {return exchangeCode;}
			set {exchangeCode = value;}
		}

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum ExchangeStatus
        {
            get{return exchangeStatus;}
            set{exchangeStatus = value;}
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
            get { return this.exchangeId;}
            set { this.exchangeId = value;}
        }
        
        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get{return exchangeStatus;}
            set{exchangeStatus = value;}
        }
        /// <summary>
        /// 数据状态名
        /// </summary>
        public string ExchangeStatusName
        {
            get { return this.exchangeStatus.ToString(); }
        }
        
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.ExchangeDAL";
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