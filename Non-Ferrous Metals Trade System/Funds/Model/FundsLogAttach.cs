
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FundsLogAttach.cs
// 文件功能描述：资金流水附件dbo.Fun_FundsLogAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
	/// <summary>
	/// 资金流水附件dbo.Fun_FundsLogAttach实体类。
	/// </summary>
	[Serializable]
	public class FundsLogAttach : IModel
	{
		#region 字段
        
		private int fundsLogAttachId;
		private int attachId;
		private int fundsLogId;
        private string tableName = "dbo.Fun_FundsLogAttach";
		#endregion
		
		#region 构造函数
        
		public FundsLogAttach()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 资金流水附件序号
        /// </summary>
		public int FundsLogAttachId
		{
			get {return fundsLogAttachId;}
			set {fundsLogAttachId = value;}
		}

        /// <summary>
        /// 附件序号
        /// </summary>
		public int AttachId
		{
			get {return attachId;}
			set {attachId = value;}
		}

        /// <summary>
        /// 资金流水序号
        /// </summary>
		public int FundsLogId
		{
			get {return fundsLogId;}
			set {fundsLogId = value;}
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
            get { return this.fundsLogAttachId;}
            set { this.fundsLogAttachId = value;}
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
        
        private string dalName = "NFMT.Funds.DAL.FundsLogAttachDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }
        
        private string assName = "NFMT.Funds";
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