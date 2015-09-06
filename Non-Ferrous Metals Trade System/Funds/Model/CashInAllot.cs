
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInAllot.cs
// 文件功能描述：收款分配dbo.Fun_CashInAllot实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月18日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
	/// <summary>
	/// 收款分配dbo.Fun_CashInAllot实体类。
	/// </summary>
	[Serializable]
	public class CashInAllot : IModel
	{
		#region 字段
        
		private int allotId;
		private decimal allotBala;
		private int allotType;
		private int currencyId;
		private string allotDesc = String.Empty;
		private int alloter;
		private DateTime allotTime;
        private Common.StatusEnum allotStatus;
		private int allotFrom;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.Fun_CashInAllot";
		#endregion
		
		#region 构造函数
        
		public CashInAllot()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 收款分配序号
        /// </summary>
		public int AllotId
		{
			get {return allotId;}
			set {allotId = value;}
		}

        /// <summary>
        /// 分配金额
        /// </summary>
		public decimal AllotBala
		{
			get {return allotBala;}
			set {allotBala = value;}
		}

        /// <summary>
        /// 分配类型
        /// </summary>
		public int AllotType
		{
			get {return allotType;}
			set {allotType = value;}
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
        /// 备注
        /// </summary>
		public string AllotDesc
		{
			get {return allotDesc;}
			set {allotDesc = value;}
		}

        /// <summary>
        /// 分配人
        /// </summary>
		public int Alloter
		{
			get {return alloter;}
			set {alloter = value;}
		}

        /// <summary>
        /// 分配时间
        /// </summary>
		public DateTime AllotTime
		{
			get {return allotTime;}
			set {allotTime = value;}
		}

        /// <summary>
        /// 收款分配状态
        /// </summary>
        public Common.StatusEnum AllotStatus
        {
            get{return allotStatus;}
            set{allotStatus = value;}
        }
        /// <summary>
        /// 分配来源
        /// </summary>
		public int AllotFrom
		{
			get {return allotFrom;}
			set {allotFrom = value;}
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
            get { return this.allotId;}
            set { this.allotId = value;}
        }
        
        
        
        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get{return allotStatus;}
            set{allotStatus = value;}
        }
        /// <summary>
        /// 数据状态名
        /// </summary>
        public string AllotStatusName
        {
            get { return this.allotStatus.ToString(); }
        }
        
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        
        private string dalName = "NFMT.Funds.DAL.CashInAllotDAL";
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