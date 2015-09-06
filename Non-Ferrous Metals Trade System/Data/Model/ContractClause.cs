
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractClause.cs
// 文件功能描述：合约条款dbo.ContractClause实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
	/// <summary>
	/// 合约条款dbo.ContractClause实体类。
	/// </summary>
	[Serializable]
	public class ContractClause : IModel
	{
		#region 字段
        
		private int clauseId;
		private string clauseText = String.Empty;
		private string clauseEnText = String.Empty;
        private Common.StatusEnum clauseStatus;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.ContractClause";
        
		#endregion
		
		#region 构造函数
        
		public ContractClause()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 合约条款序号
        /// </summary>
		public int ClauseId
		{
			get {return clauseId;}
			set {clauseId = value;}
		}

        /// <summary>
        /// 条款内容
        /// </summary>
		public string ClauseText
		{
			get {return clauseText;}
			set {clauseText = value;}
		}

        /// <summary>
        /// 条款英文内容
        /// </summary>
		public string ClauseEnText
		{
			get {return clauseEnText;}
			set {clauseEnText = value;}
		}

        /// <summary>
        /// 条款状态
        /// </summary>
        public Common.StatusEnum ClauseStatus
        {
            get{return clauseStatus;}
            set{clauseStatus = value;}
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
            get { return this.clauseId;}
            set { this.clauseId = value;}
        }
        
        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get{return clauseStatus;}
            set{clauseStatus = value;}
        }
        /// <summary>
        /// 数据状态名
        /// </summary>
        public string ClauseStatusName
        {
            get { return this.clauseStatus.ToString(); }
        }
        
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.ContractClauseDAL";
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