
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InterestDetail.cs
// 文件功能描述：利息明细dbo.Pri_InterestDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2015年3月20日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
    /// <summary>
    /// 利息明细dbo.Pri_InterestDetail实体类。
    /// </summary>
    [Serializable]
    public class InterestDetail : IModel
    {
        #region 字段

        private int detailId;
        private int interestId;
        private int subContractId;
        private int contractId;
        private int stockId;
        private int stockLogId;
        private decimal interestAmount;
        private decimal pricingUnit;
        private decimal premium;
        private decimal otherPrice;
        private decimal interestPrice;
        private decimal stockBala;
        private DateTime interestStartDate;
        private DateTime interestEndDate;
        private int interestDay;
        private decimal interestUnit;
        private decimal interestBala;
        private int interestType;
        private StatusEnum detailStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Pri_InterestDetail";
        #endregion

        #region 构造函数

        public InterestDetail()
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
        /// 利息序号
        /// </summary>
        public int InterestId
        {
            get { return interestId; }
            set { interestId = value; }
        }

        /// <summary>
        /// 子合约序号
        /// </summary>
        public int SubContractId
        {
            get { return subContractId; }
            set { subContractId = value; }
        }

        /// <summary>
        /// 合约序号
        /// </summary>
        public int ContractId
        {
            get { return contractId; }
            set { contractId = value; }
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
        /// 库存流水序号
        /// </summary>
        public int StockLogId
        {
            get { return stockLogId; }
            set { stockLogId = value; }
        }

        /// <summary>
        /// 计息重量
        /// </summary>
        public decimal InterestAmount
        {
            get { return interestAmount; }
            set { interestAmount = value; }
        }

        /// <summary>
        /// 期货点价价格
        /// </summary>
        public decimal PricingUnit
        {
            get { return pricingUnit; }
            set { pricingUnit = value; }
        }

        /// <summary>
        /// 升贴水
        /// </summary>
        public decimal Premium
        {
            get { return premium; }
            set { premium = value; }
        }

        /// <summary>
        /// 其他费用
        /// </summary>
        public decimal OtherPrice
        {
            get { return otherPrice; }
            set { otherPrice = value; }
        }

        /// <summary>
        /// 计息价格
        /// </summary>
        public decimal InterestPrice
        {
            get { return interestPrice; }
            set { interestPrice = value; }
        }

        /// <summary>
        /// 计息货值
        /// </summary>
        public decimal StockBala
        {
            get { return stockBala; }
            set { stockBala = value; }
        }

        /// <summary>
        /// 起息日
        /// </summary>
        public DateTime InterestStartDate
        {
            get { return interestStartDate; }
            set { interestStartDate = value; }
        }

        /// <summary>
        /// 到期日
        /// </summary>
        public DateTime InterestEndDate
        {
            get { return interestEndDate; }
            set { interestEndDate = value; }
        }

        /// <summary>
        /// 计息天数
        /// </summary>
        public int InterestDay
        {
            get { return interestDay; }
            set { interestDay = value; }
        }

        /// <summary>
        /// 日利息额
        /// </summary>
        public decimal InterestUnit
        {
            get { return interestUnit; }
            set { interestUnit = value; }
        }

        /// <summary>
        /// 当前息额
        /// </summary>
        public decimal InterestBala
        {
            get { return interestBala; }
            set { interestBala = value; }
        }

        /// <summary>
        /// 计息类型
        /// </summary>
        public int InterestType
        {
            get { return interestType; }
            set { interestType = value; }
        }

        /// <summary>
        /// 明细状态
        /// </summary>
        public StatusEnum DetailStatus
        {
            get { return detailStatus; }
            set { detailStatus = value; }
        }

        /// <summary>
        /// 申请创建人
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
        /// 最后修改人
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

        private string dalName = "NFMT.DoPrice.DAL.InterestDetailDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.DoPrice";
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