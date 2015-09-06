
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockOutDetail.cs
// 文件功能描述：出库明细dbo.St_StockOutDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月16日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 出库明细dbo.St_StockOutDetail实体类。
    /// </summary>
    [Serializable]
    public class StockOutDetail : IModel
    {
        #region 字段

        private int detailId;
        private int stockOutId;
        private StatusEnum detailStatus;
        private int stockId;
        private int stockOutApplyDetailId;
        private decimal netAmount;
        private int stockLogId;
        private decimal grossAmount;
        private int bundles;
        private string tableName = "dbo.St_StockOutDetail";
        #endregion

        #region 构造函数

        public StockOutDetail()
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
        /// 出库序号
        /// </summary>
        public int StockOutId
        {
            get { return stockOutId; }
            set { stockOutId = value; }
        }

        /// <summary>
        /// 出库明细状态
        /// </summary>
        public StatusEnum DetailStatus
        {
            get { return detailStatus; }
            set { detailStatus = value; }
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
        /// 出库申请明细序号
        /// </summary>
        public int StockOutApplyDetailId
        {
            get { return stockOutApplyDetailId; }
            set { stockOutApplyDetailId = value; }
        }

        /// <summary>
        /// 出库净量
        /// </summary>
        public decimal NetAmount
        {
            get { return netAmount; }
            set { netAmount = value; }
        }

        /// <summary>
        /// 出库流水序号
        /// </summary>
        public int StockLogId
        {
            get { return stockLogId; }
            set { stockLogId = value; }
        }

        /// <summary>
        /// 出库毛重
        /// </summary>
        public decimal GrossAmount
        {
            get { return grossAmount; }
            set { grossAmount = value; }
        }

        /// <summary>
        /// 捆数
        /// </summary>
        public int Bundles
        {
            get { return bundles; }
            set { bundles = value; }
        }

        public int CreatorId { get; set; }
        public DateTime CreateTime { get; set; }
        public int LastModifyId { get; set; }
        public DateTime LastModifyTime { get; set; }

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

        private string dalName = "NFMT.WareHouse.DAL.StockOutDetailDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.WareHouse";
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