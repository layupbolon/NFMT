
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FuturesPrice.cs
// 文件功能描述：期货价格表dbo.FuturesPrice实体类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
    /// <summary>
    /// 期货价格表dbo.FuturesPrice实体类。
    /// </summary>
    [Serializable]
    public class FuturesPrice : IModel
    {
        #region 字段

        private int fPId;
        private DateTime tradeDate;
        private int futuresCodeId;
        private Common.StatusEnum futuresStatus;
        private DateTime deliverDate;
        private decimal settlePrice;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.FuturesPrice";

        #endregion

        #region 构造函数

        public FuturesPrice()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int FPId
        {
            get { return fPId; }
            set { fPId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime TradeDate
        {
            get { return tradeDate; }
            set { tradeDate = value; }
        }

        /// <summary>
        /// 期货合约序号
        /// </summary>
        public int FuturesCodeId
        {
            get { return futuresCodeId; }
            set { futuresCodeId = value; }
        }

        public string TradeCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum FuturesStatus
        {
            get { return futuresStatus; }
            set { futuresStatus = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DeliverDate
        {
            get { return deliverDate; }
            set { deliverDate = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal SettlePrice
        {
            get { return settlePrice; }
            set { settlePrice = value; }
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
            get { return this.fPId; }
            set { this.fPId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return futuresStatus; }
            set { futuresStatus = value; }
        }
        /// <summary>
        /// 数据状态名
        /// </summary>
        public string FuturesStatusName
        {
            get { return this.futuresStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.FuturesPriceDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Data";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT_Basic";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}