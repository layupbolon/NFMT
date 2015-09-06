
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AuthOperate.cs
// 文件功能描述：操作权限表dbo.AuthOperate实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
    /// <summary>
    /// 操作权限表dbo.AuthOperate实体类。
    /// </summary>
    [Serializable]
    public class AuthOperate : IModel
    {
        #region 字段

        private int authOperateId;
        private string operateCode = String.Empty;
        private string operateName = String.Empty;
        private int operateType;
        private int menuId;
        private int empId;
        private Common.StatusEnum authOperateStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.AuthOperate";
        #endregion

        #region 构造函数

        public AuthOperate()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 操作权限序号
        /// </summary>
        public int AuthOperateId
        {
            get { return authOperateId; }
            set { authOperateId = value; }
        }

        /// <summary>
        /// 操作权限编号
        /// </summary>
        public string OperateCode
        {
            get { return operateCode; }
            set { operateCode = value; }
        }

        /// <summary>
        /// 操作权限名称
        /// </summary>
        public string OperateName
        {
            get { return operateName; }
            set { operateName = value; }
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public int OperateType
        {
            get { return operateType; }
            set { operateType = value; }
        }

        /// <summary>
        /// 菜单序号
        /// </summary>
        public int MenuId
        {
            get { return menuId; }
            set { menuId = value; }
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
        /// 
        /// </summary>
        public Common.StatusEnum AuthOperateStatus
        {
            get { return authOperateStatus; }
            set { authOperateStatus = value; }
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
            get { return this.authOperateId; }
            set { this.authOperateId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return authOperateStatus; }
            set { authOperateStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.User.DAL.AuthOperateDAL";
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