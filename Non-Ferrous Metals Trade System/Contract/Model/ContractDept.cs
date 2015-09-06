
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractDept.cs
// 文件功能描述：合约执行部门明细dbo.Con_ContractDept实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月25日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Contract.Model
{
	/// <summary>
	/// 合约执行部门明细dbo.Con_ContractDept实体类。
	/// </summary>
	[Serializable]
	public class ContractDept : IModel
	{
		#region 字段
        
		private int detailId;
		private int contractId;
		private int deptId;
        private Common.StatusEnum detailStatus;
        private string tableName = "dbo.Con_ContractDept";
		#endregion
		
		#region 构造函数
        
		public ContractDept()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 明细序号
        /// </summary>
		public int DetailId
		{
            get { return detailId; }
            set { detailId = value; }
		}

        /// <summary>
        /// 合约序号
        /// </summary>
		public int ContractId
		{
            get { return contractId; }
            set { contractId = value; }
		}

        /// <summary>
        /// 部门序号
        /// </summary>
		public int DeptId
		{
            get { return deptId; }
            set { deptId = value; }
		}

        /// <summary>
        /// 明细状态
        /// </summary>
        public Common.StatusEnum DetailStatus
		{
            get { return detailStatus; }
            set { detailStatus = value; }
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
            get { return this.detailId; }
            set { this.detailId = value; }
        }
        
        
        
        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return detailStatus; }
            set { detailStatus = value; }
        }
        
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        
        private string dalName = "NFMT.Contract.DAL.ContractDeptDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }
        
        private string assName = "NFMT.Contract";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }        
        }
        
        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
		#endregion
	}
}