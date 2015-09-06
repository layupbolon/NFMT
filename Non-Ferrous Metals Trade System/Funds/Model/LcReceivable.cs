
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：LcReceivable.cs
// 文件功能描述：信用证收款登记dbo.Fun_LcReceivable_Ref实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
	/// <summary>
	/// 信用证收款登记dbo.Fun_LcReceivable_Ref实体类。
	/// </summary>
	[Serializable]
	public class LcReceivable : IModel
	{
		#region 字段
        
		private int refId;
		private int receId;
		private int lcId;
        private string tableName = "dbo.Fun_LcReceivable_Ref";
		#endregion
		
		#region 构造函数
        
		public LcReceivable()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 信用证收款序号
        /// </summary>
		public int RefId
		{
			get {return refId;}
			set {refId = value;}
		}

        /// <summary>
        /// 收款登记序号
        /// </summary>
		public int ReceId
		{
			get {return receId;}
			set {receId = value;}
		}

        /// <summary>
        /// 信用证序号
        /// </summary>
		public int LcId
		{
			get {return lcId;}
			set {lcId = value;}
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
            get { return this.refId;}
            set { this.refId = value;}
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
        
        private string dalName = "NFMT.Funds.DAL.LcReceivableDAL";
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