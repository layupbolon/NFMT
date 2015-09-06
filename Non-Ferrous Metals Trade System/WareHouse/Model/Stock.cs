
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Stock.cs
// 文件功能描述：库存dbo.St_Stock实体类。
// 创建人：CodeSmith
// 创建时间： 2015年1月19日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 库存dbo.St_Stock实体类。
    /// </summary>
    [Serializable]
    public class Stock : IModel
    {
        #region 字段

        private int stockId;
        private int stockNameId;
        private string stockNo = String.Empty;
        private DateTime stockDate;
        private int assetId;
        private int bundles;
        private decimal grossAmount;
        private decimal netAmount;
        private decimal receiptInGap;
        private decimal receiptOutGap;
        private decimal curGrossAmount;
        private decimal curNetAmount;
        private int uintId;
        private int deliverPlaceId;
        private int brandId;
        private int customsType;
        private int groupId;
        private int corpId;
        private int deptId;
        private int producerId;
        private string paperNo = String.Empty;
        private int paperHolder;
        private string format = String.Empty;
        private int originPlaceId;
        private string originPlace = String.Empty;
        private StockStatusEnum preStatus;
        private StockStatusEnum stockStatus;
        private string cardNo = String.Empty;
        private string memo = String.Empty;
        private int stockType;
        private string tableName = "dbo.St_Stock";
        #endregion

        #region 构造函数

        public Stock()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 库存序号
        /// </summary>
        public int StockId
        {
            get { return stockId; }
            set { stockId = value; }
        }

        /// <summary>
        /// 库存名称序号
        /// </summary>
        public int StockNameId
        {
            get { return stockNameId; }
            set { stockNameId = value; }
        }

        /// <summary>
        /// 库存编号
        /// </summary>
        public string StockNo
        {
            get { return stockNo; }
            set { stockNo = value; }
        }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime StockDate
        {
            get { return stockDate; }
            set { stockDate = value; }
        }

        /// <summary>
        /// 品种序号
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
        /// 入库毛量，单据毛重
        /// </summary>
        public decimal GrossAmount
        {
            get { return grossAmount; }
            set { grossAmount = value; }
        }

        /// <summary>
        /// 入库净量，单据净重
        /// </summary>
        public decimal NetAmount
        {
            get { return netAmount; }
            set { netAmount = value; }
        }

        /// <summary>
        /// 入库回执磅差
        /// </summary>
        public decimal ReceiptInGap
        {
            get { return receiptInGap; }
            set { receiptInGap = value; }
        }

        /// <summary>
        /// 出库回执磅差
        /// </summary>
        public decimal ReceiptOutGap
        {
            get { return receiptOutGap; }
            set { receiptOutGap = value; }
        }

        /// <summary>
        /// 当前毛吨
        /// </summary>
        public decimal CurGrossAmount
        {
            get { return curGrossAmount; }
            set { curGrossAmount = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal CurNetAmount
        {
            get { return curNetAmount; }
            set { curNetAmount = value; }
        }

        /// <summary>
        /// 计量单位
        /// </summary>
        public int UintId
        {
            get { return uintId; }
            set { uintId = value; }
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
        /// 品牌
        /// </summary>
        public int BrandId
        {
            get { return brandId; }
            set { brandId = value; }
        }

        /// <summary>
        /// 报关状态
        /// </summary>
        public int CustomsType
        {
            get { return customsType; }
            set { customsType = value; }
        }

        /// <summary>
        /// 所属集团
        /// </summary>
        public int GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        /// <summary>
        /// 所属公司
        /// </summary>
        public int CorpId
        {
            get { return corpId; }
            set { corpId = value; }
        }

        /// <summary>
        /// 所属部门
        /// </summary>
        public int DeptId
        {
            get { return deptId; }
            set { deptId = value; }
        }

        /// <summary>
        /// 生产商序号
        /// </summary>
        public int ProducerId
        {
            get { return producerId; }
            set { producerId = value; }
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
        /// 规格
        /// </summary>
        public string Format
        {
            get { return format; }
            set { format = value; }
        }

        /// <summary>
        /// 产地序号
        /// </summary>
        public int OriginPlaceId
        {
            get { return originPlaceId; }
            set { originPlaceId = value; }
        }

        /// <summary>
        /// 产地
        /// </summary>
        public string OriginPlace
        {
            get { return originPlace; }
            set { originPlace = value; }
        }

        /// <summary>
        /// 前一状态
        /// </summary>
        public StockStatusEnum PreStatus
        {
            get { return preStatus; }
            set { preStatus = value; }
        }

        /// <summary>
        /// 库存状态
        /// </summary>
        public StockStatusEnum StockStatus
        {
            get { return stockStatus; }
            set { stockStatus = value; }
        }

        public string StockStatusName
        {
            get { return this.stockStatus.ToString(); }
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
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        /// <summary>
        /// 库存类型(提报货)
        /// </summary>
        public int StockType
        {
            get { return stockType; }
            set { stockType = value; }
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
            get { return this.stockId; }
            set { this.stockId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get;
            set;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.StockDAL";
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