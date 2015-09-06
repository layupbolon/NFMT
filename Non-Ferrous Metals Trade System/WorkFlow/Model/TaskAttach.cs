
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：TaskAttach.cs
// 文件功能描述：任务附件dbo.Wf_TaskAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月31日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WorkFlow.Model
{
	/// <summary>
	/// 任务附件dbo.Wf_TaskAttach实体类。
	/// </summary>
	[Serializable]
	public class TaskAttach : IModel
	{
		#region 字段
        
		private int taskAttachId;
		private int taskId;
		private int attachId;
        private string tableName = "dbo.Wf_TaskAttach";
		#endregion
		
		#region 构造函数
        
		public TaskAttach()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 任务附件序号
        /// </summary>
		public int TaskAttachId
		{
			get {return taskAttachId;}
			set {taskAttachId = value;}
		}

        /// <summary>
        /// 任务序号
        /// </summary>
		public int TaskId
		{
			get {return taskId;}
			set {taskId = value;}
		}

        /// <summary>
        /// 附件主表序号
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
            get { return this.taskAttachId;}
            set { this.taskAttachId = value;}
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
        
        private string dalName = "NFMT.WorkFlow.DAL.TaskAttachDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }
        
        private string assName = "NFMT.WorkFlow";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }        
        }
        
        public string StatusName
        {
            get { return this.Status.ToString(); }
        }
        
        private string dataBaseName = "NFMT_WorkFlow";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
		#endregion
	}
}