
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Funds.cs
// 文件功能描述：资金dbo.Fun_Funds实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
	/// <summary>
	/// 资金dbo.Fun_Funds实体类。
	/// </summary>
	[Serializable]
	public class Funds : IModel
	{
		#region 字段
        
		private int fundsId;
		private int groupId;
		private int corpId;
		private decimal fundsValue;
		private int currencyId;
		private int fundsType;
        private string tableName = "dbo.Fun_Funds";
		#endregion
		
		#region 构造函数
        
		public Funds()
		{
		}
		
		#endregion
		
		#region 属性
		
        /// <summary>
        /// 资金序号
        /// </summary>
		public int FundsId
		{
			get {return fundsId;}
			set {fundsId = value;}
		}

        /// <summary>
        /// 集团序号
        /// </summary>
		public int GroupId
		{
			get {return groupId;}
			set {groupId = value;}
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
        /// 资金数量
        /// </summary>
		public decimal FundsValue
		{
			get {return fundsValue;}
			set {fundsValue = value;}
		}

        /// <summary>
        /// 币种
        /// </summary>
		public int CurrencyId
		{
			get {return currencyId;}
			set {currencyId = value;}
		}

        /// <summary>
        /// 资金类型
        /// </summary>
		public int FundsType
		{
			get {return fundsType;}
			set {fundsType = value;}
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
            get { return this.fundsId;}
            set { this.fundsId = value;}
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
        
        private string dalName = "NFMT.Funds.DAL.FundsDAL";
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