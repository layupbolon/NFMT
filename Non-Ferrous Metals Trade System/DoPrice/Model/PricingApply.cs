
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingApply.cs
// 文件功能描述：点价申请dbo.Pri_PricingApply实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月8日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
    /// <summary>
    /// 点价申请dbo.Pri_PricingApply实体类。
    /// </summary>
    [Serializable]
    public class PricingApply : IModel
    {
        #region 字段

        private int pricingApplyId;
        private int applyId;
        private int subContractId;
        private int contractId;
        private int pricingDirection;
        private DateTime qPDate;
        private decimal delayAmount;
        private decimal delayFee;
        private DateTime delayQPDate;
        private decimal otherFee;
        private string otherDesc = String.Empty;
        private DateTime startTime;
        private DateTime endTime;
        private decimal minPrice;
        private decimal maxPrice;
        private int currencyId;
        private int pricingBlocId;
        private int pricingCorpId;
        private decimal pricingWeight;
        private int mUId;
        private int assertId;
        private int pricingPersoinId;
        private int pricingStyle;
        private DateTime declareDate;
        private DateTime avgPriceStart;
        private DateTime avgPriceEnd;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Pri_PricingApply";
        #endregion

        #region 构造函数

        public PricingApply()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 点价申请序号
        /// </summary>
        public int PricingApplyId
        {
            get { return pricingApplyId; }
            set { pricingApplyId = value; }
        }

        /// <summary>
        /// 申请主表序号
        /// </summary>
        public int ApplyId
        {
            get { return applyId; }
            set { applyId = value; }
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
        /// 点价方向
        /// </summary>
        public int PricingDirection
        {
            get { return pricingDirection; }
            set { pricingDirection = value; }
        }

        /// <summary>
        /// QP日期
        /// </summary>
        public DateTime QPDate
        {
            get { return qPDate; }
            set { qPDate = value; }
        }

        /// <summary>
        /// 延期总量
        /// </summary>
        public decimal DelayAmount
        {
            get { return delayAmount; }
            set { delayAmount = value; }
        }

        /// <summary>
        /// 延期总费
        /// </summary>
        public decimal DelayFee
        {
            get { return delayFee; }
            set { delayFee = value; }
        }

        /// <summary>
        /// 延期QP
        /// </summary>
        public DateTime DelayQPDate
        {
            get { return delayQPDate; }
            set { delayQPDate = value; }
        }

        /// <summary>
        /// 其他费
        /// </summary>
        public decimal OtherFee
        {
            get { return otherFee; }
            set { otherFee = value; }
        }

        /// <summary>
        /// 其他费描述
        /// </summary>
        public string OtherDesc
        {
            get { return otherDesc; }
            set { otherDesc = value; }
        }

        /// <summary>
        /// 点价起始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        /// <summary>
        /// 点价最终时间
        /// </summary>
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        /// <summary>
        /// 点价最低均价
        /// </summary>
        public decimal MinPrice
        {
            get { return minPrice; }
            set { minPrice = value; }
        }

        /// <summary>
        /// 点价最高均价
        /// </summary>
        public decimal MaxPrice
        {
            get { return maxPrice; }
            set { maxPrice = value; }
        }

        /// <summary>
        /// 价格币种
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }

        /// <summary>
        /// 点价集团
        /// </summary>
        public int PricingBlocId
        {
            get { return pricingBlocId; }
            set { pricingBlocId = value; }
        }

        /// <summary>
        /// 点价公司
        /// </summary>
        public int PricingCorpId
        {
            get { return pricingCorpId; }
            set { pricingCorpId = value; }
        }

        /// <summary>
        /// 点价重量
        /// </summary>
        public decimal PricingWeight
        {
            get { return pricingWeight; }
            set { pricingWeight = value; }
        }

        /// <summary>
        /// 重量单位
        /// </summary>
        public int MUId
        {
            get { return mUId; }
            set { mUId = value; }
        }

        /// <summary>
        /// 点价品种
        /// </summary>
        public int AssertId
        {
            get { return assertId; }
            set { assertId = value; }
        }

        /// <summary>
        /// 点价权限人
        /// </summary>
        public int PricingPersoinId
        {
            get { return pricingPersoinId; }
            set { pricingPersoinId = value; }
        }

        /// <summary>
        /// 点价方式
        /// </summary>
        public int PricingStyle
        {
            get { return pricingStyle; }
            set { pricingStyle = value; }
        }

        /// <summary>
        /// 宣布日
        /// </summary>
        public DateTime DeclareDate
        {
            get { return declareDate; }
            set { declareDate = value; }
        }

        /// <summary>
        /// 均价起始计价日
        /// </summary>
        public DateTime AvgPriceStart
        {
            get { return avgPriceStart; }
            set { avgPriceStart = value; }
        }

        /// <summary>
        /// 均价终止计价日
        /// </summary>
        public DateTime AvgPriceEnd
        {
            get { return avgPriceEnd; }
            set { avgPriceEnd = value; }
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
            get { return this.pricingApplyId; }
            set { this.pricingApplyId = value; }
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

        private string dalName = "NFMT.DoPrice.DAL.PricingApplyDAL";
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