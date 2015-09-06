
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：EmployeeContact.cs
// 文件功能描述：联系人员工关系表dbo.EmployeeContact实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
    /// <summary>
    /// 联系人员工关系表dbo.EmployeeContact实体类。
    /// </summary>
    [Serializable]
    public class EmployeeContact : IModel
    {
        #region 字段

        private int eCId;
        private int contactId;
        private int empId;
        private Common.StatusEnum refStatus;
        private string tableName = "dbo.EmployeeContact";

        #endregion

        #region 构造函数

        public EmployeeContact()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int ECId
        {
            get { return eCId; }
            set { eCId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ContactId
        {
            get { return contactId; }
            set { contactId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        /// <summary>
        /// 关联状态
        /// </summary>
        public Common.StatusEnum RefStatus
        {
            get { return refStatus; }
            set { refStatus = value; }
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
            get { return this.eCId; }
            set { this.eCId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return refStatus; }
            set { refStatus = value; }
        }
        /// <summary>
        /// 数据状态名
        /// </summary>
        public string RefStatusName
        {
            get { return this.refStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }


        private string dalName = "NFMT.User.DAL.EmployeeContactDAL";
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