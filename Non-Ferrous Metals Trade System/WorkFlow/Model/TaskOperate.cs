
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：TaskOperate.cs
// 文件功能描述：任务操作表dbo.Wf_TaskOperate实体类。
// 创建人：CodeSmith
// 创建时间： 2015年4月17日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WorkFlow.Model
{
    /// <summary>
    /// 任务操作表dbo.Wf_TaskOperate实体类。
    /// </summary>
    [Serializable]
    public class TaskOperate : IModel
    {
        #region 字段

        private int taskOperateId;
        private int taskNodeId;
        private string operateUrl = String.Empty;
        private Common.StatusEnum operateStatus;
        private string tableName = "dbo.Wf_TaskOperate";
        #endregion

        #region 构造函数

        public TaskOperate()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public int TaskOperateId
        {
            get { return taskOperateId; }
            set { taskOperateId = value; }
        }

        /// <summary>
        /// 任务节点序号
        /// </summary>
        public int TaskNodeId
        {
            get { return taskNodeId; }
            set { taskNodeId = value; }
        }

        /// <summary>
        /// 操作页面地址
        /// </summary>
        public string OperateUrl
        {
            get { return operateUrl; }
            set { operateUrl = value; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public Common.StatusEnum OperateStatus
        {
            get { return operateStatus; }
            set { operateStatus = value; }
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
            get { return this.taskOperateId; }
            set { this.taskOperateId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return operateStatus; }
            set { operateStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WorkFlow.DAL.TaskOperateDAL";
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