
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Pricing.cs
// 文件功能描述：点价表dbo.Pri_Pricing实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月8日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
    /// <summary>
    /// 点价表dbo.Pri_Pricing实体类。
    /// </summary>
    [Serializable]
    public class Pricing : IModel
    {
        #region 字段

        private int pricingId;
        private int pricingApplyId;
        private decimal pricingWeight;
        private int mUId;
        private int exchangeId;
        private int futuresCodeId;
        private DateTime futuresCodeEndDate;
        private DateTime spotQP;
        private decimal delayFee;
        private decimal spread;
        private decimal otherFee;
        private decimal avgPrice;
        private DateTime pricingTime;
        private int currencyId;
        private int pricinger;
        private int assertId;
        private int pricingDirection;
        private Common.StatusEnum pricingStatus;
        private decimal finalPrice;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Pri_Pricing";
        #endregion

        #region 构造函数

        public Pricing()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 点价序号
        /// </summary>
        public int PricingId
        {
            get { return pricingId; }
            set { pricingId = value; }
        }

        /// <summary>
        /// 点价申请序号
        /// </summary>
        public int PricingApplyId
        {
            get { return pricingApplyId; }
            set { pricingApplyId = value; }
        }

        /// <summary>
        /// 点价重量
        /// </summary>
        public decimal PricingWeight
        {
            get { return pricingWeight; }
            set { pricingWeight = value; }
        }

        /// <summary>
        /// 点价重量单位
        /// </summary>
        public int MUId
        {
            get { return mUId; }
            set { mUId = value; }
        }

        /// <summary>
        /// 点价期货市场
        /// </summary>
        public int ExchangeId
        {
            get { return exchangeId; }
            set { exchangeId = value; }
        }

        /// <summary>
        /// 点价期货合约
        /// </summary>
        public int FuturesCodeId
        {
            get { return futuresCodeId; }
            set { futuresCodeId = value; }
        }

        /// <summary>
        /// 期货合约到期日
        /// </summary>
        public DateTime FuturesCodeEndDate
        {
            get { return futuresCodeEndDate; }
            set { futuresCodeEndDate = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime SpotQP
        {
            get { return spotQP; }
            set { spotQP = value; }
        }

        /// <summary>
        /// 延期费
        /// </summary>
        public decimal DelayFee
        {
            get { return delayFee; }
            set { delayFee = value; }
        }

        /// <summary>
        /// 调期费
        /// </summary>
        public decimal Spread
        {
            get { return spread; }
            set { spread = value; }
        }

        /// <summary>
        /// 其他费
        /// </summary>
        public decimal OtherFee
        {
            get { return otherFee; }
            set { otherFee = value; }
        }

        /// <summary>
        /// 其他费描述
        /// </summary>
        public decimal AvgPrice
        {
            get { return avgPrice; }
            set { avgPrice = value; }
        }

        /// <summary>
        /// 点价时间
        /// </summary>
        public DateTime PricingTime
        {
            get { return pricingTime; }
            set { pricingTime = value; }
        }

        /// <summary>
        /// 币种
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }

        /// <summary>
        /// 点价人
        /// </summary>
        public int Pricinger
        {
            get { return pricinger; }
            set { pricinger = value; }
        }

        /// <summary>
        /// 点价品种
        /// </summary>
        public int AssertId
        {
            get { return assertId; }
            set { assertId = value; }
        }

        /// <summary>
        /// 点价方向
        /// </summary>
        public int PricingDirection
        {
            get { return pricingDirection; }
            set { pricingDirection = value; }
        }

        /// <summary>
        /// 点价状态
        /// </summary>
        public Common.StatusEnum PricingStatus
        {
            get { return pricingStatus; }
            set { pricingStatus = value; }
        }

        /// <summary>
        /// 最终价格
        /// </summary>
        public decimal FinalPrice
        {
            get { return finalPrice; }
            set { finalPrice = value; }
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
        /// 最后修改人
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
            get { return this.pricingId; }
            set { this.pricingId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return pricingStatus; }
            set { pricingStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.DoPrice.DAL.PricingDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.DoPrice";
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