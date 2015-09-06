
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMove.cs
// 文件功能描述：移库dbo.St_StockMove实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月28日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 移库dbo.St_StockMove实体类。
    /// </summary>
    [Serializable]
    public class StockMove : IModel
    {
        #region 字段

        private int stockMoveId;
        private int stockMoveApplyId;
        private int mover;
        private DateTime moveTime;
        private Common.StatusEnum moveStatus;
        private string moveMemo = String.Empty;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.St_StockMove";
        #endregion

        #region 构造函数

        public StockMove()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 移库序号
        /// </summary>
        public int StockMoveId
        {
            get { return stockMoveId; }
            set { stockMoveId = value; }
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
        /// 移库人
        /// </summary>
        public int Mover
        {
            get { return mover; }
            set { mover = value; }
        }

        /// <summary>
        /// 移库时间
        /// </summary>
        public DateTime MoveTime
        {
            get { return moveTime; }
            set { moveTime = value; }
        }

        /// <summary>
        /// 移库状态
        /// </summary>
        public Common.StatusEnum MoveStatus
        {
            get { return moveStatus; }
            set { moveStatus = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string MoveMemo
        {
            get { return moveMemo; }
            set { moveMemo = value; }
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
            get { return this.stockMoveId; }
            set { this.stockMoveId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return moveStatus; }
            set { moveStatus = value; }
        }

        /// <summary>
        /// 数据状态名
        /// </summary>
        public string MoveStatusName
        {
            get { return this.moveStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.StockMoveDAL";
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

        public int DeliverPlaceId { get; set; }

        public string DPName { get; set; }
    }
}