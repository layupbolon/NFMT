                                                                                                                                                                                                                                                                                                                              
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 �Ϻ�������Ϣ�Ƽ����޹�˾ ��Ȩ���С� 
// �ļ�����ContractFundsAllotStock.cs
// �ļ�������������Լ����������dbo.Fun_ContractFundsAllotStock_Refʵ���ࡣ
// �����ˣ�CodeSmith
// ����ʱ�䣺 2014��8��5��
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
	/// <summary>
	/// ��Լ����������dbo.Fun_ContractFundsAllotStock_Refʵ���ࡣ
	/// </summary>
	[Serializable]
	public class ContractFundsAllotStock : IModel
	{
		#region �ֶ�
        
		private int refId;
		private int allotId;
		private int receId;
		private int stockId;
		private decimal allotBala;
		private int currencyId;
		private int creatorId;
		private DateTime createTime;
		private int lastModifyId;
		private DateTime lastModifyTime;
        private string tableName = "dbo.Fun_ContractFundsAllotStock_Ref";
		#endregion
		
		#region ���캯��
        
		public ContractFundsAllotStock()
		{
		}
		
		#endregion
		
		#region ����
		
        /// <summary>
        /// �������
        /// </summary>
		public int RefId
		{
			get {return refId;}
			set {refId = value;}
		}

        /// <summary>
        /// �������
        /// </summary>
		public int AllotId
		{
			get {return allotId;}
			set {allotId = value;}
		}

        /// <summary>
        /// �տ�Ǽ����
        /// </summary>
		public int ReceId
		{
			get {return receId;}
			set {receId = value;}
		}

        /// <summary>
        /// ������
        /// </summary>
		public int StockId
		{
			get {return stockId;}
			set {stockId = value;}
		}

        /// <summary>
        /// ���
        /// </summary>
		public decimal AllotBala
		{
			get {return allotBala;}
			set {allotBala = value;}
		}

        /// <summary>
        /// ����
        /// </summary>
		public int CurrencyId
		{
			get {return currencyId;}
			set {currencyId = value;}
		}

        /// <summary>
        /// ���������
        /// </summary>
		public int CreatorId
		{
			get {return creatorId;}
			set {creatorId = value;}
		}

        /// <summary>
        /// ����ʱ��
        /// </summary>
		public DateTime CreateTime
		{
			get {return createTime;}
			set {createTime = value;}
		}

        /// <summary>
        /// ����޸������
        /// </summary>
		public int LastModifyId
		{
			get {return lastModifyId;}
			set {lastModifyId = value;}
		}

        /// <summary>
        /// ����޸�ʱ��
        /// </summary>
		public DateTime LastModifyTime
		{
			get {return lastModifyTime;}
			set {lastModifyTime = value;}
		}
        
        
        
        /// <summary>
        /// �������
        /// </summary>
        public int Id
        {
            get { return this.refId;}
            set { this.refId = value;}
        }
        
        
        
        /// <summary>
        /// ����״̬
        /// </summary>
        public Common.StatusEnum Status
        {
            get;set;
        }
        
        /// <summary>
        /// ����
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        
        private string dalName = "NFMT.Funds.DAL.ContractFundsAllotStockDAL";
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