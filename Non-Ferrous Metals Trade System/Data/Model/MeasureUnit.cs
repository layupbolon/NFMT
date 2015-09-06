
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：MeasureUnit.cs
// 文件功能描述：dbo.MeasureUnit实体类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
	/// <summary>
	/// dbo.MeasureUnit实体类。
	/// </summary>
	[Serializable]
	public class MeasureUnit : IModel
	{
		#region 字段
        
		private int mUId;
		private string mUName = String.Empty;
		private int baseId;
		private decimal transformRate;
        private Common.StatusEnum mUStatus;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.MeasureUnit";
        
		#endregion
		
		#region 构造函数
        
		public MeasureUnit()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 
        /// </summary>
		public int MUId
		{
			get {return mUId;}
			set {mUId = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public string MUName
		{
			get {return mUName;}
			set {mUName = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public int BaseId
		{
			get {return baseId;}
			set {baseId = value;}
		}

        public string BaseMUName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
		public decimal TransformRate
		{
			get {return transformRate;}
			set {transformRate = value;}
		}

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum MUStatus
        {
            get{return mUStatus;}
            set{mUStatus = value;}
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
            get { return this.mUId;}
            set { this.mUId = value;}
        }
        
        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get{return mUStatus;}
            set{mUStatus = value;}
        }
        /// <summary>
        /// 数据状态名
        /// </summary>
        public string MUStatusName
        {
            get { return this.mUStatus.ToString(); }
        }
        
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.MeasureUnitDAL";
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