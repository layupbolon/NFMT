
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：NodeOperate.cs
// 文件功能描述：节点操作表dbo.Wf_NodeOperate实体类。
// 创建人：CodeSmith
// 创建时间： 2015年4月17日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WorkFlow.Model
{
    /// <summary>
    /// 节点操作表dbo.Wf_NodeOperate实体类。
    /// </summary>
    [Serializable]
    public class NodeOperate : IModel
    {
        #region 字段

        private int operateId;
        private int nodeId;
        private string operateUrl = String.Empty;
        private Common.StatusEnum operateStatus;
        private string tableName = "dbo.Wf_NodeOperate";
        #endregion

        #region 构造函数

        public NodeOperate()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public int OperateId
        {
            get { return operateId; }
            set { operateId = value; }
        }

        /// <summary>
        /// 节点序号
        /// </summary>
        public int NodeId
        {
            get { return nodeId; }
            set { nodeId = value; }
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
            get { return this.operateId; }
            set { this.operateId = value; }
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

        private string dalName = "NFMT.WorkFlow.DAL.NodeOperateDAL";
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