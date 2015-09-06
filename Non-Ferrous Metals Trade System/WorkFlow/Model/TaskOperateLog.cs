
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Wf_TaskOperateLog.cs
// 文件功能描述：任务操作记录表dbo.Wf_TaskOperateLog实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月11日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WorkFlow.Model
{
    /// <summary>
    /// 任务操作记录表dbo.Wf_TaskOperateLog实体类。
    /// </summary>
    [Serializable]
    public class TaskOperateLog : IModel
    {
        #region 字段

        private int logId;
        private int taskNodeId;
        private int empId;
        private string memo = String.Empty;
        private DateTime logTime;
        private string logResult = String.Empty;
        private string tableName = "dbo.Wf_TaskOperateLog";

        #endregion

        #region 构造函数

        public TaskOperateLog()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int LogId
        {
            get { return logId; }
            set { logId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TaskNodeId
        {
            get { return taskNodeId; }
            set { taskNodeId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LogTime
        {
            get { return logTime; }
            set { logTime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LogResult
        {
            get { return logResult; }
            set { logResult = value; }
        }

        public int CreatorId { get; set; }
        public DateTime CreateTime { get; set; }
        public int LastModifyId { get; set; }
        public DateTime LastModifyTime { get; set; }

        /// <summary>
        /// 数据序号
        /// </summary>
        public int Id
        {
            get { return this.logId; }
            set { this.logId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get;
            set;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WorkFlow.DAL.TaskOperateLogDAL";
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