
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AuditEmp.cs
// 文件功能描述：审核人表dbo.Wf_AuditEmp实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月12日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WorkFlow.Model
{
    /// <summary>
    /// 审核人表dbo.Wf_AuditEmp实体类。
    /// </summary>
    [Serializable]
    public class AuditEmp : IModel
    {
        #region 字段

        private int auditEmpId;
        private int auditEmpType;
        private int valueId;
        private Common.StatusEnum auditEmpStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Wf_AuditEmp";
        #endregion

        #region 构造函数

        public AuditEmp()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public int AuditEmpId
        {
            get { return auditEmpId; }
            set { auditEmpId = value; }
        }

        /// <summary>
        /// 审核人类型
        /// </summary>
        public int AuditEmpType
        {
            get { return auditEmpType; }
            set { auditEmpType = value; }
        }

        /// <summary>
        /// 审核类型内容
        /// </summary>
        public int ValueId
        {
            get { return valueId; }
            set { valueId = value; }
        }

        /// <summary>
        /// 审核类型状态
        /// </summary>
        public Common.StatusEnum AuditEmpStatus
        {
            get { return auditEmpStatus; }
            set { auditEmpStatus = value; }
        }

        /// <summary>
        /// 创建人序号
        /// </summary>
        public int CreatorId
        {
            get { return creatorId; }
            set { creatorId = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 最后修改人序号
        /// </summary>
        public int LastModifyId
        {
            get { return lastModifyId; }
            set { lastModifyId = value; }
        }

        /// <summary>
        /// 最后修改时间
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
            get { return this.auditEmpId; }
            set { this.auditEmpId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return auditEmpStatus; }
            set { auditEmpStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WorkFlow.DAL.AuditEmpDAL";
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