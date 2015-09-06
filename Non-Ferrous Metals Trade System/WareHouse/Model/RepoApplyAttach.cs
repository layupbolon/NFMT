
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoApplyAttach.cs
// 文件功能描述：回购申请附件dbo.St_RepoApplyAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月27日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
	/// <summary>
	/// 回购申请附件dbo.St_RepoApplyAttach实体类。
	/// </summary>
	[Serializable]
	public class RepoApplyAttach : IModel
	{
		#region 字段
        
		private int repoApplyAttachId;
		private int repoApplyId;
		private int attachId;
        private string tableName = "dbo.St_RepoApplyAttach";
		#endregion
		
		#region 构造函数
        
		public RepoApplyAttach()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 回购申请附件序号
        /// </summary>
		public int RepoApplyAttachId
		{
			get {return repoApplyAttachId;}
			set {repoApplyAttachId = value;}
		}

        /// <summary>
        /// 回购申请序号
        /// </summary>
		public int RepoApplyId
		{
			get {return repoApplyId;}
			set {repoApplyId = value;}
		}

        /// <summary>
        /// 附件序号
        /// </summary>
		public int AttachId
		{
			get {return attachId;}
			set {attachId = value;}
		}
        
        public int CreatorId{get;set;}
public DateTime CreateTime{get;set;}
public int LastModifyId{get;set;}
public DateTime LastModifyTime{get;set;}
        
        /// <summary>
        /// 数据序号
        /// </summary>
        public int Id
        {
            get { return this.repoApplyAttachId;}
            set { this.repoApplyAttachId = value;}
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
        
        private string dalName = "NFMT.WareHouse.DAL.RepoApplyAttachDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }
        
        private string assName = "NFMT.WareHouse";
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