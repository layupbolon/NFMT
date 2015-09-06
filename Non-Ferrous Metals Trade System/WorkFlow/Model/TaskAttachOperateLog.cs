
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：TaskAttachOperateLog.cs
// 文件功能描述：任务附件操作记录表dbo.Wf_TaskAttachOperateLog实体类。
// 创建人：CodeSmith
// 创建时间： 2015年1月4日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WorkFlow.Model
{
	/// <summary>
	/// 任务附件操作记录表dbo.Wf_TaskAttachOperateLog实体类。
	/// </summary>
	[Serializable]
	public class TaskAttachOperateLog : IModel
	{
		#region 字段
        
		private int operateLogId;
		private int logId;
		private int attachId;
        private string tableName = "dbo.Wf_TaskAttachOperateLog";
		#endregion
		
		#region 构造函数
        
		public TaskAttachOperateLog()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 序号
        /// </summary>
		public int OperateLogId
		{
			get {return operateLogId;}
			set {operateLogId = value;}
		}

        /// <summary>
        /// 记录序号
        /// </summary>
		public int LogId
		{
			get {return logId;}
			set {logId = value;}
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
            get { return this.operateLogId;}
            set { this.operateLogId = value;}
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
        
        private string dalName = "NFMT.WorkFlow.DAL.TaskAttachOperateLogDAL";
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