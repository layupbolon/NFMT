
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingDetail.cs
// 文件功能描述：点价明细表dbo.Pri_PricingDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月8日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
    /// <summary>
    /// 点价明细表dbo.Pri_PricingDetail实体类。
    /// </summary>
    [Serializable]
    public class PricingDetail : IModel
    {
        #region 字段

        private int detailId;
        private int pricingId;
        private int pricingApplyId;
        private int pricingApplyDetailId;
        private int stockId;
        private int stockLogId;
        private decimal pricingWeight;
        private decimal avgPrice;
        private DateTime pricingTime;
        private int pricinger;
        private Common.StatusEnum detailStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Pri_PricingDetail";
        #endregion

        #region 构造函数

        public PricingDetail()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 点价明细序号
        /// </summary>
        public int DetailId
        {
            get { return detailId; }
            set { detailId = value; }
        }

        /// <summary>
        /// 点价申请序号
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
        /// 申请明细序号
        /// </summary>
        public int PricingApplyDetailId
        {
            get { return pricingApplyDetailId; }
            set { pricingApplyDetailId = value; }
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
        /// 库存流水序号
        /// </summary>
        public int StockLogId
        {
            get { return stockLogId; }
            set { stockLogId = value; }
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
        /// 点价均价
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
        /// 点价人
        /// </summary>
        public int Pricinger
        {
            get { return pricinger; }
            set { pricinger = value; }
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

        private string dalName = "NFMT.DoPrice.DAL.PricingDetailDAL";
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