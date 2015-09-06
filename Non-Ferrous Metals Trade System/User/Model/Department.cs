
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Department.cs
// 文件功能描述：部门dbo.Department实体类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
    /// <summary>
    /// 部门dbo.Department实体类。
    /// </summary>
    [Serializable]
    public class Department : IModel
    {
        #region 字段

        private int deptId;
        private int corpId;
        private string deptCode = String.Empty;
        private string deptName = String.Empty;
        private string deptFullName = String.Empty;
        private string deptShort = String.Empty;
        private int deptType;
        private int parentLeve;
        private Common.StatusEnum deptStatus;
        private int deptLevel;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Department";

        #endregion

        #region 构造函数

        public Department()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int DeptId
        {
            get { return deptId; }
            set { deptId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CorpId
        {
            get { return corpId; }
            set { corpId = value; }
        }

        public string CorpName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DeptFullName
        {
            get { return deptFullName; }
            set { deptFullName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DeptShort
        {
            get { return deptShort; }
            set { deptShort = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DeptType
        {
            get { return deptType; }
            set { deptType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ParentLeve
        {
            get { return parentLeve; }
            set { parentLeve = value; }
        }

        public string ParentDeptName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum DeptStatus
        {
            get { return deptStatus; }
            set { deptStatus = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DeptLevel
        {
            get { return deptLevel; }
            set { deptLevel = value; }
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
            get { return this.deptId; }
            set { this.deptId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return deptStatus; }
            set { deptStatus = value; }
        }

        /// <summary>
        /// 数据状态名
        /// </summary>
        public string DeptStatusName
        {
            get { return this.deptStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.User.DAL.DepartmentDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.User";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT_User";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}