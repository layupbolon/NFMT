
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Employee.cs
// 文件功能描述：员工表dbo.Employee实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月30日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
	/// <summary>
	/// 员工表dbo.Employee实体类。
	/// </summary>
	[Serializable]
	public class Employee : IModel
	{
		#region 字段
        
		private int empId;
		private int deptId;
		private string empCode = String.Empty;
		private string name = String.Empty;
		private bool sex;
		private DateTime birthDay;
		private string telephone = String.Empty;
		private string phone = String.Empty;
		private int workStatus;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.Employee";
		#endregion
		
		#region 构造函数
        
		public Employee()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 员工编号
        /// </summary>
		public int EmpId
		{
			get {return empId;}
			set {empId = value;}
		}

        /// <summary>
        /// 部门序号
        /// </summary>
		public int DeptId
		{
			get {return deptId;}
			set {deptId = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public string EmpCode
		{
			get {return empCode;}
			set {empCode = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public string Name
		{
			get {return name;}
			set {name = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public bool Sex
		{
			get {return sex;}
			set {sex = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public DateTime BirthDay
		{
			get {return birthDay;}
			set {birthDay = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public string Telephone
		{
			get {return telephone;}
			set {telephone = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public string Phone
		{
			get {return phone;}
			set {phone = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public int WorkStatus
		{
			get {return workStatus;}
			set {workStatus = value;}
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
            get { return this.empId;}
            set { this.empId = value;}
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
        
        private string dalName = "NFMT.User.DAL.EmployeeDAL";
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