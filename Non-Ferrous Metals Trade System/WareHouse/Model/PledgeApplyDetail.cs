
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApplyDetail.cs
// 文件功能描述：质押申请明细dbo.St_PledgeApplyDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月27日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 质押申请明细dbo.St_PledgeApplyDetail实体类。
    /// </summary>
    [Serializable]
    public class PledgeApplyDetail : IModel
    {
        #region 字段

        private int detailId;
        private int pledgeApplyId;
        private int stockId;
        private Common.StatusEnum detailStatus;
        private decimal applyQty;
        private int uintId;
        private decimal pledgePrice;
        private int currencyId;
        private string tableName = "dbo.St_PledgeApplyDetail";
        #endregion

        #region 构造函数

        public PledgeApplyDetail()
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
        /// 质押申请序号
        /// </summary>
        public int PledgeApplyId
        {
            get { return pledgeApplyId; }
            set { pledgeApplyId = value; }
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
        /// 明细状态
        /// </summary>
        public Common.StatusEnum DetailStatus
        {
            get { return detailStatus; }
            set { detailStatus = value; }
        }

        /// <summary>
        /// 申请数量
        /// </summary>
        public decimal ApplyQty
        {
            get { return applyQty; }
            set { applyQty = value; }
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
        /// 质押价格
        /// </summary>
        public decimal PledgePrice
        {
            get { return pledgePrice; }
            set { pledgePrice = value; }
        }

        /// <summary>
        /// 币种
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
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

        private string dalName = "NFMT.WareHouse.DAL.PledgeApplyDetailDAL";
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