
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Parameter.cs
// 文件功能描述：参数表dbo.Parameter实体类。
// 创建人：CodeSmith
// 创建时间： 2015年7月1日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WorkFlow.Model
{
    /// <summary>
    /// 参数表dbo.Parameter实体类。
    /// </summary>
    [Serializable]
    public class Parameter : IModel
    {
        #region 字段

        private int paraId;
        private string paraName = String.Empty;
        private string paraValue = String.Empty;
        private Common.StatusEnum paraStatus;
        private string tableName = "dbo.Parameter";
        #endregion

        #region 构造函数

        public Parameter()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public int ParaId
        {
            get { return paraId; }
            set { paraId = value; }
        }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParaName
        {
            get { return paraName; }
            set { paraName = value; }
        }

        /// <summary>
        /// 参数值
        /// </summary>
        public string ParaValue
        {
            get { return paraValue; }
            set { paraValue = value; }
        }

        /// <summary>
        /// 参数状态
        /// </summary>
        public Common.StatusEnum ParaStatus
        {
            get { return paraStatus; }
            set { paraStatus = value; }
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
            get { return this.paraId; }
            set { this.paraId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return paraStatus; }
            set { paraStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WorkFlow.DAL.ParameterDAL";
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

        private string dataBaseName = "NFMT_Basic";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}