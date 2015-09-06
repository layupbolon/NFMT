
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Node.cs
// 文件功能描述：节点表dbo.Wf_Node实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月12日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WorkFlow.Model
{
    /// <summary>
    /// 节点表dbo.Wf_Node实体类。
    /// </summary>
    [Serializable]
    public class Node : IModel
    {
        #region 字段

        private int nodeId;
        private int masterId;
        private int nodeStatus;
        private string nodeName = String.Empty;
        private int nodeType;
        private bool isFirst;
        private bool isLast;
        private int preNodeId;
        private int auditEmpId;
        private int authGroupId;
        private int nodeLevel;
        private string tableName = "dbo.Wf_Node";
        #endregion

        #region 构造函数

        public Node()
        {
        }

        #endregion

        #region 属性

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
        public int MasterId
        {
            get { return masterId; }
            set { masterId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int NodeStatus
        {
            get { return nodeStatus; }
            set { nodeStatus = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string NodeName
        {
            get { return nodeName; }
            set { nodeName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int NodeType
        {
            get { return nodeType; }
            set { nodeType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFirst
        {
            get { return isFirst; }
            set { isFirst = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLast
        {
            get { return isLast; }
            set { isLast = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PreNodeId
        {
            get { return preNodeId; }
            set { preNodeId = value; }
        }

        /// <summary>
        /// 审核人序号
        /// </summary>
        public int AuditEmpId
        {
            get { return auditEmpId; }
            set { auditEmpId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AuthGroupId
        {
            get { return authGroupId; }
            set { authGroupId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int NodeLevel
        {
            get { return nodeLevel; }
            set { nodeLevel = value; }
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
            get { return this.nodeId; }
            set { this.nodeId = value; }
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

        private string dalName = "NFMT.WorkFlow.DAL.NodeDAL";
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