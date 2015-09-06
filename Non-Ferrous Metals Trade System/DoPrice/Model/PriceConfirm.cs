
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PriceConfirm.cs
// 文件功能描述：价格确认表dbo.Pri_PriceConfirm实体类。
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
    /// <summary>
    /// 价格确认表dbo.Pri_PriceConfirm实体类。
    /// </summary>
    [Serializable]
    public class PriceConfirm : IModel
    {
        #region 字段

        private int priceConfirmId;
        private int outCorpId;
        private int inCorpId;
        private int contractId;
        private int subId;
        private decimal contractAmount;
        private decimal subAmount;
        private decimal realityAmount;
        private decimal pricingAvg;
        private decimal premiumAvg;
        private decimal interestAvg;
        private decimal otherAvg;
        private decimal interestBala;
        private decimal settlePrice;
        private decimal settleBala;
        private DateTime pricingDate;
        private int currencyId;
        private int unitId;
        private int takeCorpId;
        private string contactPerson = String.Empty;
        private string memo = String.Empty;
        private Common.StatusEnum priceConfirmStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Pri_PriceConfirm";
        #endregion

        #region 构造函数

        public PriceConfirm()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 价格确认序号
        /// </summary>
        public int PriceConfirmId
        {
            get { return priceConfirmId; }
            set { priceConfirmId = value; }
        }

        /// <summary>
        /// 对方公司
        /// </summary>
        public int OutCorpId
        {
            get { return outCorpId; }
            set { outCorpId = value; }
        }

        /// <summary>
        /// 我方公司
        /// </summary>
        public int InCorpId
        {
            get { return inCorpId; }
            set { inCorpId = value; }
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
        /// 子合约序号
        /// </summary>
        public int SubId
        {
            get { return subId; }
            set { subId = value; }
        }

        /// <summary>
        /// 合约签订数量
        /// </summary>
        public decimal ContractAmount
        {
            get { return contractAmount; }
            set { contractAmount = value; }
        }

        /// <summary>
        /// 子合约签订数量
        /// </summary>
        public decimal SubAmount
        {
            get { return subAmount; }
            set { subAmount = value; }
        }

        /// <summary>
        /// 实际发货数量
        /// </summary>
        public decimal RealityAmount
        {
            get { return realityAmount; }
            set { realityAmount = value; }
        }

        /// <summary>
        /// 期货点价均价
        /// </summary>
        public decimal PricingAvg
        {
            get { return pricingAvg; }
            set { pricingAvg = value; }
        }

        /// <summary>
        /// 升贴水均价
        /// </summary>
        public decimal PremiumAvg
        {
            get { return premiumAvg; }
            set { premiumAvg = value; }
        }

        /// <summary>
        /// 利息均价
        /// </summary>
        public decimal InterestAvg
        {
            get { return interestAvg; }
            set { interestAvg = value; }
        }

        /// <summary>
        /// 其他均价
        /// </summary>
        public decimal OtherAvg
        {
            get { return otherAvg; }
            set { otherAvg = value; }
        }

        /// <summary>
        /// 利息总额
        /// </summary>
        public decimal InterestBala
        {
            get { return interestBala; }
            set { interestBala = value; }
        }

        /// <summary>
        /// 结算单价
        /// </summary>
        public decimal SettlePrice
        {
            get { return settlePrice; }
            set { settlePrice = value; }
        }

        /// <summary>
        /// 结算总额
        /// </summary>
        public decimal SettleBala
        {
            get { return settleBala; }
            set { settleBala = value; }
        }

        /// <summary>
        /// 选价日期
        /// </summary>
        public DateTime PricingDate
        {
            get { return pricingDate; }
            set { pricingDate = value; }
        }

        /// <summary>
        /// 币种
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }

        /// <summary>
        /// 重量单位
        /// </summary>
        public int UnitId
        {
            get { return unitId; }
            set { unitId = value; }
        }

        /// <summary>
        /// 提货单位
        /// </summary>
        public int TakeCorpId
        {
            get { return takeCorpId; }
            set { takeCorpId = value; }
        }

        /// <summary>
        /// 供方委托提货单位联系人
        /// </summary>
        public string ContactPerson
        {
            get { return contactPerson; }
            set { contactPerson = value; }
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
        /// 价格确认状态
        /// </summary>
        public Common.StatusEnum PriceConfirmStatus
        {
            get { return priceConfirmStatus; }
            set { priceConfirmStatus = value; }
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
            get { return this.priceConfirmId; }
            set { this.priceConfirmId = value; }
        }



        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return priceConfirmStatus; }
            set { priceConfirmStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.DoPrice.DAL.PriceConfirmDAL";
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