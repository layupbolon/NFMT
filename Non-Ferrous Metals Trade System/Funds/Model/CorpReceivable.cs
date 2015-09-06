
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorpReceivable.cs
// 文件功能描述：收款分配至集团或公司dbo.Fun_CorpReceivable_Ref实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
	/// <summary>
	/// 收款分配至集团或公司dbo.Fun_CorpReceivable_Ref实体类。
	/// </summary>
	[Serializable]
	public class CorpReceivable : IModel
	{
		#region 字段
        
		private int refId;
		private int allotId;
		private int detailId;
		private int blocId;
		private int corpId;
		private int recId;
		private bool isShare;
        private string tableName = "dbo.Fun_CorpReceivable_Ref";
		#endregion
		
		#region 构造函数
        
		public CorpReceivable()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 序号
        /// </summary>
		public int RefId
		{
			get {return refId;}
			set {refId = value;}
		}

        /// <summary>
        /// 分配序号
        /// </summary>
		public int AllotId
		{
			get {return allotId;}
			set {allotId = value;}
		}

        /// <summary>
        /// 明细序号
        /// </summary>
		public int DetailId
		{
			get {return detailId;}
			set {detailId = value;}
		}

        /// <summary>
        /// 集团序号
        /// </summary>
		public int BlocId
		{
			get {return blocId;}
			set {blocId = value;}
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
        /// 收款登记序号
        /// </summary>
		public int RecId
		{
			get {return recId;}
			set {recId = value;}
		}

        /// <summary>
        /// 是否集团共享资金
        /// </summary>
		public bool IsShare
		{
			get {return isShare;}
			set {isShare = value;}
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
            get;
            set;
        }
        
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        
        private string dalName = "NFMT.Funds.DAL.CorpReceivableDAL";
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