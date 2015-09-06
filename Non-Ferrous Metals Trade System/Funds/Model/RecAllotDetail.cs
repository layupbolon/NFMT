
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RecAllotDetail.cs
// 文件功能描述：收款分配明细dbo.Fun_RecAllotDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
	/// <summary>
	/// 收款分配明细dbo.Fun_RecAllotDetail实体类。
	/// </summary>
	[Serializable]
	public class RecAllotDetail : IModel
	{
		#region 字段
        
		private int detailId;
		private int recId;
		private int allotId;
		private decimal allotBala;
		private Common.StatusEnum detailStatus;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.Fun_RecAllotDetail";
		#endregion
		
		#region 构造函数
        
		public RecAllotDetail()
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
        /// 收款登记序号
        /// </summary>
		public int RecId
		{
			get {return recId;}
			set {recId = value;}
		}

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
        /// 明细状态
        /// </summary>
        public Common.StatusEnum DetailStatus
		{
			get {return detailStatus;}
			set {detailStatus = value;}
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
        
        private string dalName = "NFMT.Funds.DAL.RecAllotDetailDAL";
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