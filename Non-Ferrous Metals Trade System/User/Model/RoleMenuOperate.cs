
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RoleMenuOperate.cs
// 文件功能描述：角色菜单操作关联表dbo.RoleMenuOperate实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月29日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
    /// <summary>
    /// 角色菜单操作关联表dbo.RoleMenuOperate实体类。
    /// </summary>
    [Serializable]
    public class RoleMenuOperate : IModel
    {
        #region 字段

        private int refID;
        private int roleId;
        private int menuId;
        private StatusEnum refStatus;
        private int operateId;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.RoleMenuOperate";
        #endregion

        #region 构造函数

        public RoleMenuOperate()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 关联序号
        /// </summary>
        public int RefID
        {
            get { return refID; }
            set { refID = value; }
        }

        /// <summary>
        /// 角色序号
        /// </summary>
        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
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
        /// 关联状态
        /// </summary>
        public StatusEnum RefStatus
        {
            get { return refStatus; }
            set { refStatus = value; }
        }

        /// <summary>
        /// 操作序号
        /// </summary>
        public int OperateId
        {
            get { return operateId; }
            set { operateId = value; }
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
            get { return this.refID; }
            set { this.refID = value; }
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
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.User.DAL.RoleMenuOperateDAL";
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