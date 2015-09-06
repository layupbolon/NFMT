
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PriceConfirmDetail.cs
// 文件功能描述：价格确认明细dbo.Pri_PriceConfirmDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
    /// <summary>
    /// 价格确认明细dbo.Pri_PriceConfirmDetail实体类。
    /// </summary>
    [Serializable]
    public class PriceConfirmDetail : IModel
    {
        #region 字段

        private int detailId;
        private int priceConfirmId;
        private int interestDetailId;
        private int interestId;
        private int stockLogId;
        private int stockId;
        private decimal confirmAmount;
        private decimal settlePrice;
        private decimal settleBala;
        private Common.StatusEnum detailStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Pri_PriceConfirmDetail";
        #endregion

        #region 构造函数

        public PriceConfirmDetail()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 明细序号
        /// </summary>
        public int DetailId
        {
            get { return detailId; }
            set { detailId = value; }
        }

        /// <summary>
        /// 价格确认单序号
        /// </summary>
        public int PriceConfirmId
        {
            get { return priceConfirmId; }
            set { priceConfirmId = value; }
        }

        /// <summary>
        /// 利息明细序号
        /// </summary>
        public int InterestDetailId
        {
            get { return interestDetailId; }
            set { interestDetailId = value; }
        }

        /// <summary>
        /// 利息序号
        /// </summary>
        public int InterestId
        {
            get { return interestId; }
            set { interestId = value; }
        }

        /// <summary>
        /// 库存流水序号
        /// </summary>
        public int StockLogId
        {
            get { return stockLogId; }
            set { stockLogId = value; }
        }

        /// <summary>
        /// 库存序号
        /// </summary>
        public int StockId
        {
            get { return stockId; }
            set { stockId = value; }
        }

        /// <summary>
        /// 结算重量
        /// </summary>
        public decimal ConfirmAmount
        {
            get { return confirmAmount; }
            set { confirmAmount = value; }
        }

        /// <summary>
        /// 结算单价
        /// </summary>
        public decimal SettlePrice
        {
            get { return settlePrice; }
            set { settlePrice = value; }
        }

        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal SettleBala
        {
            get { return settleBala; }
            set { settleBala = value; }
        }

        /// <summary>
        /// 明细状态
        /// </summary>
        public Common.StatusEnum DetailStatus
        {
            get { return detailStatus; }
            set { detailStatus = value; }
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
            get { return this.detailId; }
            set { this.detailId = value; }
        }



        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return detailStatus; }
            set { detailStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.DoPrice.DAL.PriceConfirmDetailDAL";
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