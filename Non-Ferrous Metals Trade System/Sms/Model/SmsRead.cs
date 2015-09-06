
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsRead.cs
// 文件功能描述：消息已读dbo.Sm_SmsRead实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月9日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Sms.Model
{
	/// <summary>
	/// 消息已读dbo.Sm_SmsRead实体类。
	/// </summary>
	[Serializable]
	public class SmsRead : IModel
	{
		#region 字段
        
		private int smsReadId;
		private int smsId;
		private int empId;
		private DateTime lastReadTime;
		private int readStatus;
        private string tableName = "dbo.Sm_SmsRead";
		#endregion
		
		#region 构造函数
        
		public SmsRead()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 已读序号
        /// </summary>
		public int SmsReadId
		{
			get {return smsReadId;}
			set {smsReadId = value;}
		}

        /// <summary>
        /// 消息序号
        /// </summary>
		public int SmsId
		{
			get {return smsId;}
			set {smsId = value;}
		}

        /// <summary>
        /// 读取人
        /// </summary>
		public int EmpId
		{
			get {return empId;}
			set {empId = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public DateTime LastReadTime
		{
			get {return lastReadTime;}
			set {lastReadTime = value;}
		}

        /// <summary>
        /// 
        /// </summary>
		public int ReadStatus
		{
			get {return readStatus;}
			set {readStatus = value;}
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
            get { return this.smsReadId;}
            set { this.smsReadId = value;}
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
        
        private string dalName = "NFMT.Sms.DAL.SmsReadDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }
        
        private string assName = "NFMT.Sms";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }        
        }
        
        public string StatusName
        {
            get { return this.Status.ToString(); }
        }
        
        private string dataBaseName = "NFMT_Sms";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
		#endregion
	}
}