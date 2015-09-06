
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FlowMaster.cs
// 文件功能描述：流程模板dbo.Wf_FlowMaster实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月5日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WorkFlow.Model
{
    /// <summary>
    /// 流程模板dbo.Wf_FlowMaster实体类。
    /// </summary>
    [Serializable]
    public class FlowMaster : IModel
    {
        #region 字段

        private int masterId;
        private string masterName = String.Empty;
        private Common.StatusEnum masterStatus;
        private string viewUrl = String.Empty;
        private string conditionUrl = String.Empty;
        private string successUrl = String.Empty;
        private string refusalUrl = String.Empty;
        private string viewTitle = String.Empty;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Wf_FlowMaster";
        #endregion

        #region 构造函数

        public FlowMaster()
        {
        }

        #endregion

        #region 属性

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
        public string MasterName
        {
            get { return masterName; }
            set { masterName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum MasterStatus
        {
            get { return masterStatus; }
            set { masterStatus = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ViewUrl
        {
            get { return viewUrl; }
            set { viewUrl = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ConditionUrl
        {
            get { return conditionUrl; }
            set { conditionUrl = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SuccessUrl
        {
            get { return successUrl; }
            set { successUrl = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RefusalUrl
        {
            get { return refusalUrl; }
            set { refusalUrl = value; }
        }

        /// <summary>
        /// 显示标题
        /// </summary>
        public string ViewTitle
        {
            get { return viewTitle; }
            set { viewTitle = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CreatorId
        {
            get { return creatorId; }
            set { creatorId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int LastModifyId
        {
            get { return lastModifyId; }
            set { lastModifyId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastModifyTime
        {
            get { return lastModifyTime; }
            set { lastModifyTime = value; }
        }

        /// <summary>
        /// 数据序号
        /// </summary>
        public int Id
        {
            get { return this.masterId; }
            set { this.masterId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return masterStatus; }
            set { masterStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WorkFlow.DAL.FlowMasterDAL";
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