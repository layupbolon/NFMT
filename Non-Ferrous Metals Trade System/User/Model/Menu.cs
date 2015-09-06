
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Menu.cs
// 文件功能描述：功能菜单表dbo.Menu实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
    /// <summary>
    /// 功能菜单表dbo.Menu实体类。
    /// </summary>
    [Serializable]
    public class Menu : IModel
    {
        #region 字段

        private int menuId;
        private string menuName = String.Empty;
        private string menuDesc = String.Empty;
        private int parentId;
        private int firstId;
        private string url = String.Empty;
        private Common.StatusEnum menuStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Menu";
        #endregion

        #region 构造函数

        public Menu()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 菜单序号
        /// </summary>
        public int MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }

        /// <summary>
        /// 功能菜单名称
        /// </summary>
        public string MenuName
        {
            get { return menuName; }
            set { menuName = value; }
        }

        /// <summary>
        /// 功能菜单描述
        /// </summary>
        public string MenuDesc
        {
            get { return menuDesc; }
            set { menuDesc = value; }
        }

        /// <summary>
        /// 上级菜单
        /// </summary>
        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        /// <summary>
        /// 一级菜单
        /// </summary>
        public int FirstId
        {
            get { return firstId; }
            set { firstId = value; }
        }

        /// <summary>
        /// 路径
        /// </summary>
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum MenuStatus
        {
            get { return menuStatus; }
            set { menuStatus = value; }
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
            get { return this.menuId; }
            set { this.menuId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return menuStatus; }
            set { menuStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.User.DAL.MenuDAL";
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