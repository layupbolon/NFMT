
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockReceiptDetail.cs
// 文件功能描述：回执明细dbo.St_StockReceiptDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月11日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 回执明细dbo.St_StockReceiptDetail实体类。
    /// </summary>
    [Serializable]
    public class StockReceiptDetail : IModel
    {
        #region 字段

        private int detailId;
        private int receiptId;
        private int contractId;
        private int contractSubId;
        private int stockId;
        private int stockLogId;
        private decimal preNetAmount;
        private decimal receiptAmount;
        private decimal qtyMiss;
        private decimal qtyRate;
        private Common.StatusEnum detailStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.St_StockReceiptDetail";
        #endregion

        #region 构造函数

        public StockReceiptDetail()
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
        /// 回执序号
        /// </summary>
        public int ReceiptId
        {
            get { return receiptId; }
            set { receiptId = value; }
        }

        /// <summary>
        /// 合约号
        /// </summary>
        public int ContractId
        {
            get { return contractId; }
            set { contractId = value; }
        }

        /// <summary>
        /// 子合约号
        /// </summary>
        public int ContractSubId
        {
            get { return contractSubId; }
            set { contractSubId = value; }
        }

        /// <summary>
        /// 库存号
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
        /// 回执前净重
        /// </summary>
        public decimal PreNetAmount
        {
            get { return preNetAmount; }
            set { preNetAmount = value; }
        }

        /// <summary>
        /// 回执库存净重
        /// </summary>
        public decimal ReceiptAmount
        {
            get { return receiptAmount; }
            set { receiptAmount = value; }
        }

        /// <summary>
        /// 磅差重量
        /// </summary>
        public decimal QtyMiss
        {
            get { return qtyMiss; }
            set { qtyMiss = value; }
        }

        /// <summary>
        /// 磅差比例
        /// </summary>
        public decimal QtyRate
        {
            get { return qtyRate; }
            set { qtyRate = value; }
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
        /// 数据状态名
        /// </summary>
        public string DetailStatusName
        {
            get { return this.detailStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.StockReceiptDetailDAL";
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