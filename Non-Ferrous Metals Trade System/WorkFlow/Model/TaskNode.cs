
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Wf_TaskNode.cs
// 文件功能描述：任务节点dbo.Wf_TaskNode实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月11日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WorkFlow.Model
{
    /// <summary>
    /// 任务节点dbo.Wf_TaskNode实体类。
    /// </summary>
    [Serializable]
    public class TaskNode : IModel
    {
        #region 字段

        private int taskNodeId;
        private int nodeId;
        private int taskId;
        private int nodeLevel;
        //private int nodeStatus;
        private int empId;
        private DateTime auditTime;
        private string tableName = "dbo.Wf_TaskNode";
        private Common.StatusEnum status;


        #endregion

        #region 构造函数

        public TaskNode()
        {
        }

        #endregion

        #region 属性

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
        public int NodeId
        {
            get { return nodeId; }
            set { nodeId = value; }
        }

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
        public int NodeLevel
        {
            get { return nodeLevel; }
            set { nodeLevel = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public NFMT.Common.StatusEnum NodeStatus
        {
            get { return status; }
            set { status = value; }
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
        public DateTime AuditTime
        {
            get { return auditTime; }
            set { auditTime = value; }
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
            get { return this.taskNodeId; }
            set { this.taskNodeId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WorkFlow.DAL.TaskNodeDAL";
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