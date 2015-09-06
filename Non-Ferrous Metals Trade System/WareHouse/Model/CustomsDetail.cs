
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CustomsDetail.cs
// 文件功能描述：报关明细dbo.St_CustomsDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月18日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 报关明细dbo.St_CustomsDetail实体类。
    /// </summary>
    [Serializable]
    public class CustomsDetail : IModel
    {
        #region 字段

        private int detailId;
        private int customsId;
        private int customsApplyId;
        private int customsApplyDetailId;
        private int stockId;
        private decimal grossWeight;
        private decimal netWeight;
        private decimal customsPrice;
        private int deliverPlaceId;
        private string cardNo = String.Empty;
        private Common.StatusEnum detailStatus;
        private int stockLogId;
        private string tableName = "dbo.St_CustomsDetail";
        #endregion

        #region 构造函数

        public CustomsDetail()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 报关明细序号
        /// </summary>
        public int DetailId
        {
            get { return detailId; }
            set { detailId = value; }
        }

        /// <summary>
        /// 报关序号
        /// </summary>
        public int CustomsId
        {
            get { return customsId; }
            set { customsId = value; }
        }

        /// <summary>
        /// 报关申请序号
        /// </summary>
        public int CustomsApplyId
        {
            get { return customsApplyId; }
            set { customsApplyId = value; }
        }

        /// <summary>
        /// 报关申请明细序号
        /// </summary>
        public int CustomsApplyDetailId
        {
            get { return customsApplyDetailId; }
            set { customsApplyDetailId = value; }
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
        /// 报关毛重
        /// </summary>
        public decimal GrossWeight
        {
            get { return grossWeight; }
            set { grossWeight = value; }
        }

        /// <summary>
        /// 报关净重
        /// </summary>
        public decimal NetWeight
        {
            get { return netWeight; }
            set { netWeight = value; }
        }

        /// <summary>
        /// 实际报关单价
        /// </summary>
        public decimal CustomsPrice
        {
            get { return customsPrice; }
            set { customsPrice = value; }
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
        /// 卡号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
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
        /// 报关流水序号
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

        private string dalName = "NFMT.WareHouse.DAL.CustomsDetailDAL";
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