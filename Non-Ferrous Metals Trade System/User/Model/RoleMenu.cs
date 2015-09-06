
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RoleMenu.cs
// 文件功能描述：角色菜单关联表dbo.RoleMenu实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
	/// <summary>
	/// 角色菜单关联表dbo.RoleMenu实体类。
	/// </summary>
	[Serializable]
	public class RoleMenu : IModel
	{
		#region 字段
        
		private int roleMenuID;
		private int roleId;
		private int menuId;
		private int refStatus;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.RoleMenu";
		#endregion
		
		#region 构造函数
        
		public RoleMenu()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 
        /// </summary>
		public int RoleMenuID
		{
			get {return roleMenuID;}
			set {roleMenuID = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public int RoleId
		{
			get {return roleId;}
			set {roleId = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public int MenuId
		{
			get {return menuId;}
			set {menuId = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public int RefStatus
		{
			get {return refStatus;}
			set {refStatus = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public int CreatorId
		{
			get {return creatorId;}
			set {creatorId = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public DateTime CreateTime
		{
			get {return createTime;}
			set {createTime = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public int LastModifyId
		{
			get {return lastModifyId;}
			set {lastModifyId = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public DateTime LastModifyTime
		{
			get {return lastModifyTime;}
			set {lastModifyTime = value;}
		}
        
        
        
        /// <summary>
        /// 数据序号
        /// </summary>
        public int Id
        {
            get { return this.roleMenuID;}
            set { this.roleMenuID = value;}
        }
        
        
        
        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get;set;
        }
        
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        
        private string dalName = "NFMT.User.DAL.RoleMenuDAL";
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