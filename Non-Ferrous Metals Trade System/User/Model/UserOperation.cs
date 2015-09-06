
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：UserOperation.cs
// 文件功能描述：用户操作记录dbo.UserOperation实体类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
    /// <summary>
    /// 用户操作记录dbo.UserOperation实体类。
    /// </summary>
    [Serializable]
    public class UserOperation : IModel
    {
        #region 字段

        private int userOperationId;
        private int accId;
        private DateTime operationTime;
        private int menuId;
        private string operationDesc = String.Empty;
        private string tableName = "dbo.UserOperation";

        #endregion

        #region 构造函数

        public UserOperation()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int UserOperationId
        {
            get { return userOperationId; }
            set { userOperationId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AccId
        {
            get { return accId; }
            set { accId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime OperationTime
        {
            get { return operationTime; }
            set { operationTime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OperationDesc
        {
            get { return operationDesc; }
            set { operationDesc = value; }
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
            get { return this.userOperationId; }
            set { this.userOperationId = value; }
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

        public string DalName { get; set; }

        public string AssName { get; set; }

        private string dataBaseName = "NFMT_User";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}