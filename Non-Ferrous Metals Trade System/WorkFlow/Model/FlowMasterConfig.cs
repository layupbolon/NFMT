
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FlowMasterConfig.cs
// 文件功能描述：流程模版配置表dbo.Wf_FlowMasterConfig实体类。
// 创建人：CodeSmith
// 创建时间： 2015年6月3日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WorkFlow.Model
{
    /// <summary>
    /// 流程模版配置表dbo.Wf_FlowMasterConfig实体类。
    /// </summary>
    [Serializable]
    public class FlowMasterConfig : IModel
    {
        #region 字段

        private int configId;
        private int masterId;
        private bool canPassAudit;
        private bool isSeries;
        private string refuseUrl = String.Empty;
        private Common.StatusEnum configStatus;
        private string tableName = "dbo.Wf_FlowMasterConfig";
        #endregion

        #region 构造函数

        public FlowMasterConfig()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public int ConfigId
        {
            get { return configId; }
            set { configId = value; }
        }

        /// <summary>
        /// 模版序号
        /// </summary>
        public int MasterId
        {
            get { return masterId; }
            set { masterId = value; }
        }

        /// <summary>
        /// 是否直接生效
        /// </summary>
        public bool CanPassAudit
        {
            get { return canPassAudit; }
            set { canPassAudit = value; }
        }

        /// <summary>
        /// 同级节点全部审核通过 还是只要一个审核通过
        /// </summary>
        public bool IsSeries
        {
            get { return isSeries; }
            set { isSeries = value; }
        }

        /// <summary>
        /// 拒绝后提交人看到的页面
        /// </summary>
        public string RefuseUrl
        {
            get { return refuseUrl; }
            set { refuseUrl = value; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public Common.StatusEnum ConfigStatus
        {
            get { return configStatus; }
            set { configStatus = value; }
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
            get { return this.configId; }
            set { this.configId = value; }
        }



        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return configStatus; }
            set { configStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WorkFlow.DAL.FlowMasterConfigDAL";
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