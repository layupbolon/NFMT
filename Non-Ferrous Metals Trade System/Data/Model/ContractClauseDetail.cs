
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractClauseDetail.cs
// 文件功能描述：合约条款明细dbo.ContractClauseDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
	/// <summary>
	/// 合约条款明细dbo.ContractClauseDetail实体类。
	/// </summary>
	[Serializable]
	public class ContractClauseDetail : IModel
	{
		#region 字段
        
		private int clauseDetailId;
		private int clauseId;
		private int detailDisplayType;
		private int detailDataType;
		private int formatSerial;
		private string detailValue = String.Empty;
        private Common.StatusEnum detailStatus;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.ContractClauseDetail";
        
		#endregion
		
		#region 构造函数
        
		public ContractClauseDetail()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 合约条款序号
        /// </summary>
		public int ClauseDetailId
		{
			get {return clauseDetailId;}
			set {clauseDetailId = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public int ClauseId
		{
			get {return clauseId;}
			set {clauseId = value;}
		}

        /// <summary>
        /// 明细显示类型
        /// </summary>
		public int DetailDisplayType
		{
			get {return detailDisplayType;}
			set {detailDisplayType = value;}
		}

        /// <summary>
        /// 明细数据类型
        /// </summary>
		public int DetailDataType
		{
			get {return detailDataType;}
			set {detailDataType = value;}
		}

        /// <summary>
        /// 格式序号
        /// </summary>
		public int FormatSerial
		{
			get {return formatSerial;}
			set {formatSerial = value;}
		}

        /// <summary>
        /// 显示数据
        /// </summary>
		public string DetailValue
		{
			get {return detailValue;}
			set {detailValue = value;}
		}

        /// <summary>
        /// 明细状态
        /// </summary>
        public Common.StatusEnum DetailStatus
        {
            get{return detailStatus;}
            set{detailStatus = value;}
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
            get { return this.clauseDetailId;}
            set { this.clauseDetailId = value;}
        }
        
        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get{return detailStatus;}
            set{detailStatus = value;}
        }
        /// <summary>
        /// 数据状态名
        /// </summary>
        public string DetailStatusName
        {
            get { return this.detailStatus.ToString(); }
        }
        
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.ContractClauseDetailDAL";
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