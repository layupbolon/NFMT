
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockReceipt.cs
// 文件功能描述：仓库库存净重确认回执，磅差回执dbo.St_StockReceipt实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月3日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 仓库库存净重确认回执，磅差回执dbo.St_StockReceipt实体类。
    /// </summary>
    [Serializable]
    public class StockReceipt : IModel
    {
        #region 字段

        private int receiptId;
        private int contractId;
        private int contractSubId;
        private decimal preNetAmount;
        private decimal receiptAmount;
        private int unitId;
        private decimal qtyMiss;
        private decimal qtyRate;
        private DateTime receiptDate;
        private int receipter;
        private string memo = String.Empty;
        private int receiptType;
        private Common.StatusEnum receiptStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.St_StockReceipt";
        #endregion

        #region 构造函数

        public StockReceipt()
        {
        }

        #endregion

        #region 属性

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
        /// 单位
        /// </summary>
        public int UnitId
        {
            get { return unitId; }
            set { unitId = value; }
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
        /// 回执日期
        /// </summary>
        public DateTime ReceiptDate
        {
            get { return receiptDate; }
            set { receiptDate = value; }
        }

        /// <summary>
        /// 回执人
        /// </summary>
        public int Receipter
        {
            get { return receipter; }
            set { receipter = value; }
        }

        /// <summary>
        /// 回执备注
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        /// <summary>
        /// 回执方向，（入库回执，出库回执）
        /// </summary>
        public int ReceiptType
        {
            get { return receiptType; }
            set { receiptType = value; }
        }

        /// <summary>
        /// 回执状态
        /// </summary>
        public Common.StatusEnum ReceiptStatus
        {
            get { return receiptStatus; }
            set { receiptStatus = value; }
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
            get { return this.receiptId; }
            set { this.receiptId = value; }
        }



        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return receiptStatus; }
            set { receiptStatus = value; }
        }
        /// <summary>
        /// 数据状态名
        /// </summary>
        public string ReceiptStatusName
        {
            get { return this.receiptStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.StockReceiptDAL";
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