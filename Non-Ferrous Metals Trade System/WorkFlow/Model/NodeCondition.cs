
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Wf_NodeCondition.cs
// 文件功能描述：节点条件表dbo.Wf_NodeCondition实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月11日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WorkFlow.Model
{
    /// <summary>
    /// 节点条件表dbo.Wf_NodeCondition实体类。
    /// </summary>
    [Serializable]
    public class NodeCondition : IModel
    {
        #region 字段

        private int conditionId;
        private Common.StatusEnum conditionStatus;
        private int nodeId;
        private string fieldName = String.Empty;
        private string fieldValue = String.Empty;
        private int conditionType;
        private int logicType;
        private string tableName = "dbo.Wf_NodeCondition";

        #endregion

        #region 构造函数

        public NodeCondition()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int ConditionId
        {
            get { return conditionId; }
            set { conditionId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum ConditionStatus
        {
            get { return conditionStatus; }
            set { conditionStatus = value; }
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
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FieldValue
        {
            get { return fieldValue; }
            set { fieldValue = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ConditionType
        {
            get { return conditionType; }
            set { conditionType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int LogicType
        {
            get { return logicType; }
            set { logicType = value; }
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
            get { return this.conditionId; }
            set { this.conditionId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return conditionStatus; }
            set { conditionStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WorkFlow.DAL.NodeConditionDAL";
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