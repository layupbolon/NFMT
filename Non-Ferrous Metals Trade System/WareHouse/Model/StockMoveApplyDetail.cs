
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMoveApplyDetail.cs
// 文件功能描述：回购申请库存明细dbo.St_StockMoveApplyDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月26日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 回购申请库存明细dbo.St_StockMoveApplyDetail实体类。
    /// </summary>
    [Serializable]
    public class StockMoveApplyDetail : IModel
    {
        #region 字段

        private int detailId;
        private int stockMoveApplyId;
        private int stockId;
        private string paperNo = String.Empty;
        private int deliverPlaceId;
        private Common.StatusEnum detailStatus;
        private string tableName = "dbo.St_StockMoveApplyDetail";
        #endregion

        #region 构造函数

        public StockMoveApplyDetail()
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
        /// 移库申请序号
        /// </summary>
        public int StockMoveApplyId
        {
            get { return stockMoveApplyId; }
            set { stockMoveApplyId = value; }
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
        /// 权证编号
        /// </summary>
        public string PaperNo
        {
            get { return paperNo; }
            set { paperNo = value; }
        }

        /// <summary>
        /// 交货地
        /// </summary>
        public int DeliverPlaceId
        {
            get { return deliverPlaceId; }
            set { deliverPlaceId = value; }
        }

        /// <summary>
        /// 移库申请明细状态
        /// </summary>
        public Common.StatusEnum DetailStatus
        {
            get { return detailStatus; }
            set { detailStatus = value; }
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

        private string dalName = "NFMT.WareHouse.DAL.StockMoveApplyDetailDAL";
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