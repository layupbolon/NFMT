
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StopLoss.cs
// 文件功能描述：止损表dbo.Pri_StopLoss实体类。
// 创建人：CodeSmith
// 创建时间： 2014年10月23日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
    /// <summary>
    /// 止损表dbo.Pri_StopLoss实体类。
    /// </summary>
    [Serializable]
    public class StopLoss : IModel
    {
        #region 字段

        private int stopLossId;
        private int stopLossApplyId;
        private int applyId;
        private decimal stopLossWeight;
        private int mUId;
        private int exchangeId;
        private int futuresCodeId;
        private decimal avgPrice;
        private DateTime pricingTime;
        private int currencyId;
        private int stopLosser;
        private int assertId;
        private int pricingDirection;
        private Common.StatusEnum stopLossStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Pri_StopLoss";
        #endregion

        #region 构造函数

        public StopLoss()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 止损序号
        /// </summary>
        public int StopLossId
        {
            get { return stopLossId; }
            set { stopLossId = value; }
        }

        /// <summary>
        /// 止损申请序号
        /// </summary>
        public int StopLossApplyId
        {
            get { return stopLossApplyId; }
            set { stopLossApplyId = value; }
        }

        /// <summary>
        /// 申请主表序号
        /// </summary>
        public int ApplyId
        {
            get { return applyId; }
            set { applyId = value; }
        }

        /// <summary>
        /// 点价重量
        /// </summary>
        public decimal StopLossWeight
        {
            get { return stopLossWeight; }
            set { stopLossWeight = value; }
        }

        /// <summary>
        /// 止损重量单位
        /// </summary>
        public int MUId
        {
            get { return mUId; }
            set { mUId = value; }
        }

        /// <summary>
        /// 止损期货市场
        /// </summary>
        public int ExchangeId
        {
            get { return exchangeId; }
            set { exchangeId = value; }
        }

        /// <summary>
        /// 止损期货合约
        /// </summary>
        public int FuturesCodeId
        {
            get { return futuresCodeId; }
            set { futuresCodeId = value; }
        }

        /// <summary>
        /// 止损均价
        /// </summary>
        public decimal AvgPrice
        {
            get { return avgPrice; }
            set { avgPrice = value; }
        }

        /// <summary>
        /// 止损时间
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
        /// 止损人
        /// </summary>
        public int StopLosser
        {
            get { return stopLosser; }
            set { stopLosser = value; }
        }

        /// <summary>
        /// 止损品种
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
        /// 止损状态
        /// </summary>
        public Common.StatusEnum StopLossStatus
        {
            get { return stopLossStatus; }
            set { stopLossStatus = value; }
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
            get { return this.stopLossId; }
            set { this.stopLossId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return stopLossStatus; }
            set { stopLossStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.DoPrice.DAL.StopLossDAL";
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