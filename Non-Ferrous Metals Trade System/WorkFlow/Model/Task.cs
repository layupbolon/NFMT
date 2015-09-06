
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Wf_Task.cs
// 文件功能描述：任务表dbo.Wf_Task实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月11日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WorkFlow.Model
{
    /// <summary>
    /// 任务表dbo.Wf_Task实体类。
    /// </summary>
    [Serializable]
    public class Task : IModel
    {
        #region 字段

        private int taskId;
        private int masterId;
        private string taskName = String.Empty;
        private string taskConnext = String.Empty;
        private Common.StatusEnum taskStatus;
        private int dataSourceId;
        private int taskType;
        private string tableName = "dbo.Wf_Task";

        #endregion

        #region 构造函数

        public Task()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int TaskId
        {
            get { return taskId; }
            set { taskId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MasterId
        {
            get { return masterId; }
            set { masterId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TaskName
        {
            get { return taskName; }
            set { taskName = value; }
        }

        /// <summary>
        /// 任务内容
        /// </summary>
        public string TaskConnext
        {
            get { return taskConnext; }
            set { taskConnext = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum TaskStatus
        {
            get { return taskStatus; }
            set { taskStatus = value; }
        }

        public string TaskStatusName
        {
            get { return this.TaskStatus.ToString(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DataSourceId
        {
            get { return dataSourceId; }
            set { dataSourceId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TaskType
        {
            get { return taskType; }
            set { taskType = value; }
        }

        public DateTime ApplyTime { get; set; }

        public string ApplyMemo { get; set; }

        public string ViewUrl { get; set; }

        public string EmpName { get; set; }

        public string FlowDescribtion { get; set; }

        public int CreatorId { get; set; }
        public DateTime CreateTime { get; set; }
        public int LastModifyId { get; set; }
        public DateTime LastModifyTime { get; set; }

        /// <summary>
        /// 数据序号
        /// </summary>
        public int Id
        {
            get { return this.taskId; }
            set { this.taskId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return taskStatus; }
            set { taskStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WorkFlow.DAL.TaskDAL";
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