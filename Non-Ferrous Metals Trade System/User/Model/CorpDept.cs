
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorpDept.cs
// 文件功能描述：公司部门关联表dbo.CorpDept实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月17日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
	/// <summary>
	/// 公司部门关联表dbo.CorpDept实体类。
	/// </summary>
	[Serializable]
	public class CorpDept : IModel
	{
		#region 字段
        
		private int corpEmpId;
		private int deptId;
		private int corpId;
		private int refStatus;
        private string tableName = "dbo.CorpDept";
		#endregion
		
		#region 构造函数
        
		public CorpDept()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 关联表序号
        /// </summary>
		public int CorpEmpId
		{
			get {return corpEmpId;}
			set {corpEmpId = value;}
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
        /// 公司序号
        /// </summary>
		public int CorpId
		{
			get {return corpId;}
			set {corpId = value;}
		}

        /// <summary>
        /// 关联状态
        /// </summary>
		public int RefStatus
		{
			get {return refStatus;}
			set {refStatus = value;}
		}
        
        public int CreatorId{get;set;}
public DateTime CreateTime{get;set;}
public int LastModifyId{get;set;}
public DateTime LastModifyTime{get;set;}
        
        /// <summary>
        /// 数据序号
        /// </summary>
        public int Id
        {
            get { return this.corpEmpId;}
            set { this.corpEmpId = value;}
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
        
        private string dalName = "NFMT.User.DAL.CorpDeptDAL";
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