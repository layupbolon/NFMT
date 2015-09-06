
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Attach.cs
// 文件功能描述：附件dbo.Attach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月28日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Operate.Model
{
	/// <summary>
	/// 附件dbo.Attach实体类。
	/// </summary>
	[Serializable]
	public class Attach : IModel
	{
		#region 字段
        
		private int attachId;
		private string attachName = String.Empty;
		private string serverAttachName = String.Empty;
		private string attachExt = String.Empty;
		private int attachType;
		private string attachInfo = String.Empty;
		private int attachLength;
		private string attachPath = String.Empty;
        private Common.StatusEnum attachStatus;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.Attach";
		#endregion
		
		#region 构造函数
        
		public Attach()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 附件序号
        /// </summary>
		public int AttachId
		{
			get {return attachId;}
			set {attachId = value;}
		}

        /// <summary>
        /// 附件名称，不包含扩展名
        /// </summary>
		public string AttachName
		{
			get {return attachName;}
			set {attachName = value;}
		}

        /// <summary>
        /// 服务端文件名
        /// </summary>
		public string ServerAttachName
		{
			get {return serverAttachName;}
			set {serverAttachName = value;}
		}

        /// <summary>
        /// 附件扩展名
        /// </summary>
		public string AttachExt
		{
			get {return attachExt;}
			set {attachExt = value;}
		}

        /// <summary>
        /// 附件类型
        /// </summary>
		public int AttachType
		{
			get {return attachType;}
			set {attachType = value;}
		}

        /// <summary>
        /// 附件描述
        /// </summary>
		public string AttachInfo
		{
			get {return attachInfo;}
			set {attachInfo = value;}
		}

        /// <summary>
        /// 附件大小
        /// </summary>
		public int AttachLength
		{
			get {return attachLength;}
			set {attachLength = value;}
		}

        /// <summary>
        /// 附件路径
        /// </summary>
		public string AttachPath
		{
			get {return attachPath;}
			set {attachPath = value;}
		}

        /// <summary>
        /// 附件状态
        /// </summary>
		public Common.StatusEnum AttachStatus
		{
			get {return attachStatus;}
			set {attachStatus = value;}
		}

        /// <summary>
        /// 创建人序号
        /// </summary>
		public int CreatorId
		{
			get {return creatorId;}
			set {creatorId = value;}
		}

        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime
		{
			get {return createTime;}
			set {createTime = value;}
		}

        /// <summary>
        /// 最后修改人序号
        /// </summary>
		public int LastModifyId
		{
			get {return lastModifyId;}
			set {lastModifyId = value;}
		}

        /// <summary>
        /// 最后修改时间
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
            get { return this.attachId;}
            set { this.attachId = value;}
        }
        
        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return attachStatus; }
            set { attachStatus = value; }
        }
        
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        
        private string dalName = "NFMT.Operate.DAL.AttachDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }
        
        private string assName = "NFMT.Operate";
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