
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StopLossDetail.cs
// 文件功能描述：止损明细表dbo.Pri_StopLossDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年10月23日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
    /// <summary>
    /// 止损明细表dbo.Pri_StopLossDetail实体类。
    /// </summary>
    [Serializable]
    public class StopLossDetail : IModel
    {
        #region 字段

        private int detailId;
        private int stopLossId;
        private int stopLossApplyId;
        private int stopLossApplyDetailId;
        private int stockId;
        private int stockLogId;
        private decimal stopLossWeight;
        private decimal avgPrice;
        private DateTime stopLossTime;
        private int stopLosser;
        private Common.StatusEnum detailStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Pri_StopLossDetail";
        #endregion

        #region 构造函数

        public StopLossDetail()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 止损明细序号
        /// </summary>
        public int DetailId
        {
            get { return detailId; }
            set { detailId = value; }
        }

        /// <summary>
        /// 点价申请序号
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
        /// 止损申请明细序号
        /// </summary>
        public int StopLossApplyDetailId
        {
            get { return stopLossApplyDetailId; }
            set { stopLossApplyDetailId = value; }
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
        /// 止损重量
        /// </summary>
        public decimal StopLossWeight
        {
            get { return stopLossWeight; }
            set { stopLossWeight = value; }
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
        public DateTime StopLossTime
        {
            get { return stopLossTime; }
            set { stopLossTime = value; }
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

        private string dalName = "NFMT.DoPrice.DAL.StopLossDetailDAL";
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