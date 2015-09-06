/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DoPrice.cs
// 文件功能描述：点价表dbo.DoPrice实体类。
// 创建人：CodeSmith
// 创建时间： 2014年6月11日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
    /// <summary>
    /// 点价表dbo.DoPrice实体类。
    /// </summary>
    [Serializable]
    public class DoPrice : IModel
    {
        #region 字段

        private int doPriceId;
        private decimal doPriceWeight;
        private int mUId;
        private int exchangeId;
        private int futuresCodeId;
        private decimal doPrice;
        private DateTime doTime;
        private int currencyId;
        private int doer;
        private int assertId;
        private int doPriceDirection;
        private int doPriceStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.DoPrice";

        #endregion

        #region 构造函数

        public DoPrice()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int DoPriceId
        {
            get { return doPriceId; }
            set { doPriceId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal DoPriceWeight
        {
            get { return doPriceWeight; }
            set { doPriceWeight = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MUId
        {
            get { return mUId; }
            set { mUId = value; }
        }

        public string MUName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ExchangeId
        {
            get { return exchangeId; }
            set { exchangeId = value; }
        }

        public string ExchangeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int FuturesCodeId
        {
            get { return futuresCodeId; }
            set { futuresCodeId = value; }
        }

        public string TradeCodeDesc { get; set; }

        /// <summary>
        /// 点价均价
        /// </summary>
        public decimal DoPriceValue
        {
            get { return doPrice; }
            set { doPrice = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DoTime
        {
            get { return doTime; }
            set { doTime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Doer
        {
            get { return doer; }
            set { doer = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AssertId
        {
            get { return assertId; }
            set { assertId = value; }
        }

        public string AssetName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DoPriceDirection
        {
            get { return doPriceDirection; }
            set { doPriceDirection = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DoPriceStatus
        {
            get { return doPriceStatus; }
            set { doPriceStatus = value; }
        }

        public string DoPriceStatusName
        {
            get { return DoPriceStatus.ToString(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CreatorId
        {
            get { return creatorId; }
            set { creatorId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int LastModifyId
        {
            get { return lastModifyId; }
            set { lastModifyId = value; }
        }

        /// <summary>
        /// 
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
            get { return this.doPriceId; }
            set { this.doPriceId = value; }
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

        public string DalName { get; set; }

        public string AssName { get; set; }

        private string dataBaseName = "";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}