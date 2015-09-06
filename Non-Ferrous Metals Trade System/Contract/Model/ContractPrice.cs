
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractPrice.cs
// 文件功能描述：合约价格明细dbo.Con_ContractPrice实体类。
// 创建人：CodeSmith
// 创建时间： 2015年2月4日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Contract.Model
{
    /// <summary>
    /// 合约价格明细dbo.Con_ContractPrice实体类。
    /// </summary>
    [Serializable]
    public class ContractPrice : IModel
    {
        #region 字段

        private int contractPriceId;
        private int contractId;
        private decimal fixedPrice;
        private string fixedPriceMemo = String.Empty;
        private int whoDoPrice;
        private decimal almostPrice;
        private DateTime doPriceBeginDate;
        private DateTime doPriceEndDate;
        private bool isQP;
        private int priceFrom;
        private int priceStyle1;
        private int priceStyle2;
        private int marginMode;
        private decimal marginAmount;
        private string marginMemo = String.Empty;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Con_ContractPrice";
        #endregion

        #region 构造函数

        public ContractPrice()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 合约价格序号
        /// </summary>
        public int ContractPriceId
        {
            get { return contractPriceId; }
            set { contractPriceId = value; }
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
        /// 定价价格
        /// </summary>
        public decimal FixedPrice
        {
            get { return fixedPrice; }
            set { fixedPrice = value; }
        }

        /// <summary>
        /// 定价价格备注
        /// </summary>
        public string FixedPriceMemo
        {
            get { return fixedPriceMemo; }
            set { fixedPriceMemo = value; }
        }

        /// <summary>
        /// 点价方
        /// </summary>
        public int WhoDoPrice
        {
            get { return whoDoPrice; }
            set { whoDoPrice = value; }
        }

        /// <summary>
        /// 点价估价
        /// </summary>
        public decimal AlmostPrice
        {
            get { return almostPrice; }
            set { almostPrice = value; }
        }

        /// <summary>
        /// 点价起始日
        /// </summary>
        public DateTime DoPriceBeginDate
        {
            get { return doPriceBeginDate; }
            set { doPriceBeginDate = value; }
        }

        /// <summary>
        /// 点价最终日
        /// </summary>
        public DateTime DoPriceEndDate
        {
            get { return doPriceEndDate; }
            set { doPriceEndDate = value; }
        }

        /// <summary>
        /// 可否QP延期
        /// </summary>
        public bool IsQP
        {
            get { return isQP; }
            set { isQP = value; }
        }

        /// <summary>
        /// 裸价来源(交易所列表)
        /// </summary>
        public int PriceFrom
        {
            get { return priceFrom; }
            set { priceFrom = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PriceStyle1
        {
            get { return priceStyle1; }
            set { priceStyle1 = value; }
        }

        /// <summary>
        /// 作价方式2
        /// </summary>
        public int PriceStyle2
        {
            get { return priceStyle2; }
            set { priceStyle2 = value; }
        }

        /// <summary>
        /// 价格保证金方式
        /// </summary>
        public int MarginMode
        {
            get { return marginMode; }
            set { marginMode = value; }
        }

        /// <summary>
        /// 价格保证金数量
        /// </summary>
        public decimal MarginAmount
        {
            get { return marginAmount; }
            set { marginAmount = value; }
        }

        /// <summary>
        /// 价格保证金备注
        /// </summary>
        public string MarginMemo
        {
            get { return marginMemo; }
            set { marginMemo = value; }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatorId
        {
            get { return creatorId; }
            set { creatorId = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 最后修改人序号
        /// </summary>
        public int LastModifyId
        {
            get { return lastModifyId; }
            set { lastModifyId = value; }
        }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifyTime
        {
            get { return lastModifyTime; }
            set { lastModifyTime = value; }
        }

        /// <summary>
        /// 数据序号
        /// </summary>
        public int Id
        {
            get { return this.contractPriceId; }
            set { this.contractPriceId = value; }
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

        private string dalName = "NFMT.Contract.DAL.ContractPriceDAL";
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