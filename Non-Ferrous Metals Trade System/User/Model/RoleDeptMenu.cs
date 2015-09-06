
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RoleDeptMenu.cs
// 文件功能描述：角色部门菜单关联表dbo.RoleDeptMenu实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月12日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
    /// <summary>
    /// 角色部门菜单关联表dbo.RoleDeptMenu实体类。
    /// </summary>
    [Serializable]
    public class RoleDeptMenu : IModel
    {
        #region 字段

        private int roleDeptMenuID;
        private int roleDeptId;
        private int menuId;
        private Common.StatusEnum refStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.RoleDeptMenu";
        #endregion

        #region 构造函数

        public RoleDeptMenu()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 关联序号
        /// </summary>
        public int RoleDeptMenuID
        {
            get { return roleDeptMenuID; }
            set { roleDeptMenuID = value; }
        }

        /// <summary>
        /// 角色部门序号
        /// </summary>
        public int RoleDeptId
        {
            get { return roleDeptId; }
            set { roleDeptId = value; }
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
        public Common.StatusEnum RefStatus
        {
            get { return refStatus; }
            set { refStatus = value; }
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
            get { return this.roleDeptMenuID; }
            set { this.roleDeptMenuID = value; }
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

        private string dalName = "NFMT.User.DAL.RoleDeptMenuDAL";
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