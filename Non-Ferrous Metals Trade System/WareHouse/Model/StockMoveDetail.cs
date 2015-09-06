
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMoveDetail.cs
// 文件功能描述：移库明细dbo.St_StockMoveDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月16日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 移库明细dbo.St_StockMoveDetail实体类。
    /// </summary>
    [Serializable]
    public class StockMoveDetail : IModel
    {
        #region 字段

        private int detailId;
        private int stockMoveId;
        private Common.StatusEnum moveDetailStatus;
        private int stockId;
        private string paperNo = String.Empty;
        private int deliverPlaceId;
        private int stockLogId;
        private string tableName = "dbo.St_StockMoveDetail";
        #endregion

        #region 构造函数

        public StockMoveDetail()
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
        /// 移库序号
        /// </summary>
        public int StockMoveId
        {
            get { return stockMoveId; }
            set { stockMoveId = value; }
        }

        /// <summary>
        /// 移库明细状态
        /// </summary>
        public Common.StatusEnum MoveDetailStatus
        {
            get { return moveDetailStatus; }
            set { moveDetailStatus = value; }
        }

        /// <summary>
        /// 库存编号
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
        /// 移库流水序号
        /// </summary>
        public int StockLogId
        {
            get { return stockLogId; }
            set { stockLogId = value; }
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
            get { return moveDetailStatus; }
            set { moveDetailStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.StockMoveDetailDAL";
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