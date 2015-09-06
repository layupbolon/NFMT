
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SplitDocDetail.cs
// 文件功能描述：拆单明细dbo.St_SplitDocDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月18日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 拆单明细dbo.St_SplitDocDetail实体类。
    /// </summary>
    [Serializable]
    public class SplitDocDetail : IModel
    {
        #region 字段

        private int detailId;
        private int splitDocId;
        private Common.StatusEnum detailStatus;
        private string newRefNo = String.Empty;
        private int oldRefNoId;
        private int oldStockId;
        private decimal grossAmount;
        private decimal netAmount;
        private int unitId;
        private int assetId;
        private int bundles;
        private int brandId;
        private string paperNo = String.Empty;
        private int paperHolder;
        private string cardNo = String.Empty;
        private int stockLogId;
        private string memo = String.Empty;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.St_SplitDocDetail";
        #endregion

        #region 构造函数

        public SplitDocDetail()
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
        /// 拆单序号
        /// </summary>
        public int SplitDocId
        {
            get { return splitDocId; }
            set { splitDocId = value; }
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
        /// 新业务单号
        /// </summary>
        public string NewRefNo
        {
            get { return newRefNo; }
            set { newRefNo = value; }
        }

        /// <summary>
        /// 原业务单序号
        /// </summary>
        public int OldRefNoId
        {
            get { return oldRefNoId; }
            set { oldRefNoId = value; }
        }

        /// <summary>
        /// 原库存号
        /// </summary>
        public int OldStockId
        {
            get { return oldStockId; }
            set { oldStockId = value; }
        }

        /// <summary>
        /// 新单毛重
        /// </summary>
        public decimal GrossAmount
        {
            get { return grossAmount; }
            set { grossAmount = value; }
        }

        /// <summary>
        /// 新单净重
        /// </summary>
        public decimal NetAmount
        {
            get { return netAmount; }
            set { netAmount = value; }
        }

        /// <summary>
        /// 计量单位
        /// </summary>
        public int UnitId
        {
            get { return unitId; }
            set { unitId = value; }
        }

        /// <summary>
        /// 品种
        /// </summary>
        public int AssetId
        {
            get { return assetId; }
            set { assetId = value; }
        }

        /// <summary>
        /// 捆数
        /// </summary>
        public int Bundles
        {
            get { return bundles; }
            set { bundles = value; }
        }

        /// <summary>
        /// 品牌
        /// </summary>
        public int BrandId
        {
            get { return brandId; }
            set { brandId = value; }
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
        /// 单据保管人
        /// </summary>
        public int PaperHolder
        {
            get { return paperHolder; }
            set { paperHolder = value; }
        }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }

        /// <summary>
        /// 拆单流水序号
        /// </summary>
        public int StockLogId
        {
            get { return stockLogId; }
            set { stockLogId = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
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
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.SplitDocDetailDAL";
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