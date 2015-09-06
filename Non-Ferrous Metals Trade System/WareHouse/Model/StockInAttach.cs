
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockInAttach.cs
// 文件功能描述：入库登记附件dbo.St_StockInAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
	/// <summary>
	/// 入库登记附件dbo.St_StockInAttach实体类。
	/// </summary>
	[Serializable]
	public class StockInAttach : IAttachModel
	{
		#region 字段
        
		private int stockInAttachId;
		private int stockInId;
		private int attachId;
		private int attachType;
        private string tableName = "dbo.St_StockInAttach";
		#endregion
		
		#region 构造函数
        
		public StockInAttach()
		{
		}

        public StockInAttach(NFMT.Operate.AttachType _attachType)
        {
            this.attachType = (int)_attachType;
        }
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 入库登记附件序号
        /// </summary>
		public int StockInAttachId
		{
			get {return stockInAttachId;}
			set {stockInAttachId = value;}
		}

        /// <summary>
        /// 入库登记序号
        /// </summary>
		public int StockInId
		{
			get {return stockInId;}
			set {stockInId = value;}
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
        /// 附件类型
        /// </summary>
		public int AttachType
		{
			get {return attachType;}
			set {attachType = value;}
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
            get { return this.stockInAttachId;}
            set { this.stockInAttachId = value;}
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
        
        private string dalName = "NFMT.WareHouse.DAL.StockInAttachDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }
        
        private string assName = "NFMT.WareHouse";
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

        public int BussinessDataId
        {
            get { return stockInId; }
            set { stockInId = value; }
        }
    }
}