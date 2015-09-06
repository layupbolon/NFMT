
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DataSource.cs
// 文件功能描述：数据源表dbo.Wf_DataSource实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月13日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WorkFlow.Model
{
    /// <summary>
    /// 数据源表dbo.Wf_DataSource实体类。
    /// </summary>
    [Serializable]
    public class DataSource : IModel
    {
        #region 字段

        private int sourceId;
        private string baseName = String.Empty;
        private string tableCode = String.Empty;
        private Common.StatusEnum dataStatus;
        private int rowId;
        private string viewUrl = String.Empty;
        private string refusalUrl = String.Empty;
        private string successUrl = String.Empty;
        private string conditionUrl = String.Empty;
        private int empId;
        private DateTime applyTime;
        private string applyTitle = String.Empty;
        private string applyMemo = String.Empty;
        private string applyInfo = String.Empty;
        private string tableName = "dbo.Wf_DataSource";
        #endregion

        #region 构造函数

        public DataSource()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 源序号
        /// </summary>
        public int SourceId
        {
            get { return sourceId; }
            set { sourceId = value; }
        }

        /// <summary>
        /// 库名
        /// </summary>
        public string BaseName
        {
            get { return baseName; }
            set { baseName = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableCode
        {
            get { return tableCode; }
            set { tableCode = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum DataStatus
        {
            get { return dataStatus; }
            set { dataStatus = value; }
        }

        /// <summary>
        /// 表序号
        /// </summary>
        public int RowId
        {
            get { return rowId; }
            set { rowId = value; }
        }

        /// <summary>
        /// 显示页面地址
        /// </summary>
        public string ViewUrl
        {
            get { return viewUrl; }
            set { viewUrl = value; }
        }

        /// <summary>
        /// 拒绝页面地址
        /// </summary>
        public string RefusalUrl
        {
            get { return refusalUrl; }
            set { refusalUrl = value; }
        }

        /// <summary>
        /// 通过页面地址
        /// </summary>
        public string SuccessUrl
        {
            get { return successUrl; }
            set { successUrl = value; }
        }

        /// <summary>
        /// 条件页面地址
        /// </summary>
        public string ConditionUrl
        {
            get { return conditionUrl; }
            set { conditionUrl = value; }
        }

        /// <summary>
        /// 申请人序号
        /// </summary>
        public int EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime
        {
            get { return applyTime; }
            set { applyTime = value; }
        }

        /// <summary>
        /// 申请标题
        /// </summary>
        public string ApplyTitle
        {
            get { return applyTitle; }
            set { applyTitle = value; }
        }

        /// <summary>
        /// 申请附言
        /// </summary>
        public string ApplyMemo
        {
            get { return applyMemo; }
            set { applyMemo = value; }
        }

        /// <summary>
        /// 申请内容
        /// </summary>
        public string ApplyInfo
        {
            get { return applyInfo; }
            set { applyInfo = value; }
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
            get { return this.sourceId; }
            set { this.sourceId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return dataStatus; }
            set { dataStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WorkFlow.DAL.DataSourceDAL";
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