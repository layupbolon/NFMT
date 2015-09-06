
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Lc.cs
// 文件功能描述：信用证dbo.Con_Lc实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Contract.Model
{
	/// <summary>
	/// 信用证dbo.Con_Lc实体类。
	/// </summary>
	[Serializable]
	public class Lc : IModel
	{
		#region 字段
        
		private int lcId;
		private int issueBank;
		private int adviseBank;
		private DateTime issueDate;
		private int futureDay;
		private decimal lcBala;
		private int currency;
        private Common.StatusEnum lCStatus;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.Con_Lc";
        
		#endregion
		
		#region 构造函数
        
		public Lc()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 信用证序号
        /// </summary>
		public int LcId
		{
			get {return lcId;}
			set {lcId = value;}
		}

        /// <summary>
        /// 开证行
        /// </summary>
		public int IssueBank
		{
			get {return issueBank;}
			set {issueBank = value;}
		}

        public string IssueBankName { get; set; }

        /// <summary>
        /// 通知行
        /// </summary>
		public int AdviseBank
		{
			get {return adviseBank;}
			set {adviseBank = value;}
		}

        public string AviseBankName { get; set; }

        /// <summary>
        /// 开证日期
        /// </summary>
		public DateTime IssueDate
		{
			get {return issueDate;}
			set {issueDate = value;}
		}

        /// <summary>
        /// 远期天数
        /// </summary>
		public int FutureDay
		{
			get {return futureDay;}
			set {futureDay = value;}
		}

        public string FutureDayName
        {
            get { return this.FutureDay.ToString() + "天"; }
        }

        /// <summary>
        /// 信用证金额
        /// </summary>
		public decimal LcBala
		{
			get {return lcBala;}
			set {lcBala = value;}
		}

        /// <summary>
        /// 信用证状态
        /// </summary>
        public Common.StatusEnum LCStatus
        {
            get { return lCStatus; }
            set { lCStatus = value; }
        }

        /// <summary>
        /// 信用证币种
        /// </summary>
		public int Currency
		{
			get {return currency;}
			set {currency = value;}
		}

        public string CurrencyName { get; set; }

        public string LcBalaName
        {
            get { return this.LcBala.ToString() + this.CurrencyName; }
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
            get { return this.lcId;}
            set { this.lcId = value;}
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return lCStatus; }
            set { lCStatus = value; }
        }

        /// <summary>
        /// 数据状态名
        /// </summary>
        public string LCStatusName
        {
            get { return this.lCStatus.ToString(); }
        }
        
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Contract.DAL.LcDAL";
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
            get { return this.dataBaseName; }
        }
		#endregion
	}
}