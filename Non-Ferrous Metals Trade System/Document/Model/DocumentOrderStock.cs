
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentOrderStock.cs
// 文件功能描述：制单指令库存明细dbo.Doc_DocumentOrderStock实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Document.Model
{
    /// <summary>
    /// 制单指令库存明细dbo.Doc_DocumentOrderStock实体类。
    /// </summary>
    [Serializable]
    public class DocumentOrderStock : IModel
    {
        #region 字段

        private int detailId;
        private int comDetailId;
        private int orderId;
        private int stockId;
        private int stockNameId;
        private string refNo = String.Empty;
        private decimal applyAmount;
        private StatusEnum detailStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Doc_DocumentOrderStock";
        #endregion

        #region 构造函数

        public DocumentOrderStock()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 关联序号
        /// </summary>
        public int DetailId
        {
            get { return detailId; }
            set { detailId = value; }
        }

        /// <summary>
        /// 临票库存序号
        /// </summary>
        public int ComDetailId
        {
            get { return comDetailId; }
            set { comDetailId = value; }
        }

        /// <summary>
        /// 指令序号
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
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
        /// 业务单序号
        /// </summary>
        public int StockNameId
        {
            get { return stockNameId; }
            set { stockNameId = value; }
        }

        /// <summary>
        /// 业务单号
        /// </summary>
        public string RefNo
        {
            get { return refNo; }
            set { refNo = value; }
        }

        /// <summary>
        /// 申请重量
        /// </summary>
        public decimal ApplyAmount
        {
            get { return applyAmount; }
            set { applyAmount = value; }
        }

        /// <summary>
        /// 关联数据状态
        /// </summary>
        public StatusEnum DetailStatus
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
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Document.DAL.DocumentOrderStockDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Document";
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