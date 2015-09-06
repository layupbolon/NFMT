
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FuturesCode.cs
// 文件功能描述：期货合约dbo.FuturesCode实体类。
// 创建人：CodeSmith
// 创建时间： 2014年10月14日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
    /// <summary>
    /// 期货合约dbo.FuturesCode实体类。
    /// </summary>
    [Serializable]
    public class FuturesCode : IModel
    {
        #region 字段

        private int futuresCodeId;
        private int exchageId;
        private decimal codeSize;
        private DateTime firstTradeDate;
        private DateTime lastTradeDate;
        private int mUId;
        private int currencyId;
        private int assetId;
        private StatusEnum futuresCodeStatus;
        private string tradeCode = String.Empty;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.FuturesCode";
        #endregion

        #region 构造函数

        public FuturesCode()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 期货合约序号
        /// </summary>
        public int FuturesCodeId
        {
            get { return futuresCodeId; }
            set { futuresCodeId = value; }
        }

        /// <summary>
        /// 交易所序号
        /// </summary>
        public int ExchageId
        {
            get { return exchageId; }
            set { exchageId = value; }
        }

        /// <summary>
        /// 合约规模
        /// </summary>
        public decimal CodeSize
        {
            get { return codeSize; }
            set { codeSize = value; }
        }

        /// <summary>
        /// 交易起始日期
        /// </summary>
        public DateTime FirstTradeDate
        {
            get { return firstTradeDate; }
            set { firstTradeDate = value; }
        }

        /// <summary>
        /// 交易终止日期
        /// </summary>
        public DateTime LastTradeDate
        {
            get { return lastTradeDate; }
            set { lastTradeDate = value; }
        }

        /// <summary>
        /// 合约单位
        /// </summary>
        public int MUId
        {
            get { return mUId; }
            set { mUId = value; }
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
        /// 品种
        /// </summary>
        public int AssetId
        {
            get { return assetId; }
            set { assetId = value; }
        }

        /// <summary>
        /// 合约状态
        /// </summary>
        public StatusEnum FuturesCodeStatus
        {
            get { return futuresCodeStatus; }
            set { futuresCodeStatus = value; }
        }

        /// <summary>
        /// 交易代码
        /// </summary>
        public string TradeCode
        {
            get { return tradeCode; }
            set { tradeCode = value; }
        }

        /// <summary>
        /// 创建人序号
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
            get { return this.futuresCodeId; }
            set { this.futuresCodeId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return futuresCodeStatus; }
            set { futuresCodeStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.FuturesCodeDAL";
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