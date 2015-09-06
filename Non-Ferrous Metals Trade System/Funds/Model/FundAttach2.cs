
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FundAttach2.cs
// 文件功能描述：资金附件dbo.Fun_FundAttach2实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
	/// <summary>
	/// 资金附件dbo.Fun_FundAttach2实体类。
	/// </summary>
	[Serializable]
	public class FundAttach2 : IModel
	{
		#region 字段
        
		private int fundsAttachId;
		private int attachId;
		private int fundsId;
        private string tableName = "dbo.Fun_FundAttach2";
		#endregion
		
		#region 构造函数
        
		public FundAttach2()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 资金附件序号
        /// </summary>
		public int FundsAttachId
		{
			get {return fundsAttachId;}
			set {fundsAttachId = value;}
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
		public int FundsId
		{
			get {return fundsId;}
			set {fundsId = value;}
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
            get { return this.fundsAttachId;}
            set { this.fundsAttachId = value;}
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
        
        private string dalName = "NFMT.Funds.DAL.FundAttach2DAL";
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