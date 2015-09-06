
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockIn.cs
// 文件功能描述：入库登记dbo.St_StockIn实体类。
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 入库登记dbo.St_StockIn实体类。
    /// </summary>
    [Serializable]
    public class StockIn : IModel
    {
        #region 字段

        private int stockInId;
        private int groupId;
        private int corpId;
        private int deptId;
        private DateTime stockInDate;
        private int customType;
        private decimal grossAmount;
        private decimal netAmount;
        private int uintId;
        private int assetId;
        private int bundles;
        private int brandId;
        private int deliverPlaceId;
        private int producerId;
        private int stockType;
        private int stockOperateType;
        private string paperNo = String.Empty;
        private int paperHolder;
        private string cardNo = String.Empty;
        private string format = String.Empty;
        private int originPlaceId;
        private string originPlace = String.Empty;
        private StatusEnum stockInStatus;
        private string refNo = String.Empty;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.St_StockIn";
        #endregion

        #region 构造函数

        public StockIn()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 登记序号
        /// </summary>
        public int StockInId
        {
            get { return stockInId; }
            set { stockInId = value; }
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
        /// 入库日期
        /// </summary>
        public DateTime StockInDate
        {
            get { return stockInDate; }
            set { stockInDate = value; }
        }

        /// <summary>
        /// 报关状态
        /// </summary>
        public int CustomType
        {
            get { return customType; }
            set { customType = value; }
        }

        /// <summary>
        /// 库存毛量
        /// </summary>
        public decimal GrossAmount
        {
            get { return grossAmount; }
            set { grossAmount = value; }
        }

        /// <summary>
        /// 库存净量
        /// </summary>
        public decimal NetAmount
        {
            get { return netAmount; }
            set { netAmount = value; }
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
        /// 品牌
        /// </summary>
        public int BrandId
        {
            get { return brandId; }
            set { brandId = value; }
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
        /// 生产商序号
        /// </summary>
        public int ProducerId
        {
            get { return producerId; }
            set { producerId = value; }
        }

        /// <summary>
        /// 库存类型(提报货)
        /// </summary>
        public int StockType
        {
            get { return stockType; }
            set { stockType = value; }
        }

        /// <summary>
        /// 出入库类型
        /// </summary>
        public int StockOperateType
        {
            get { return stockOperateType; }
            set { stockOperateType = value; }
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
        /// 入库状态
        /// </summary>
        public StatusEnum StockInStatus
        {
            get { return stockInStatus; }
            set { stockInStatus = value; }
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
            get { return this.stockInId; }
            set { this.stockInId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return stockInStatus; }
            set { stockInStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.StockInDAL";
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