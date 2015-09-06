
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Interest.cs
// 文件功能描述：利息表dbo.Pri_Interest实体类。
// 创建人：CodeSmith
// 创建时间： 2015年3月17日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
    /// <summary>
    /// 利息表dbo.Pri_Interest实体类。
    /// </summary>
    [Serializable]
    public class Interest : IModel
    {
        #region 字段

        private int interestId;
        private int subContractId;
        private int contractId;
        private int currencyId;
        private decimal pricingUnit;
        private decimal premium;
        private decimal otherPrice;
        private decimal interestPrice;
        private decimal payCapital;
        private decimal curCapital;
        private decimal interestRate;
        private decimal interestBala;
        private decimal interestAmountDay;
        private decimal interestAmount;
        private int unit;
        private int interestStyle;
        private string memo = String.Empty;
        private DateTime interestDate;
        private StatusEnum interestStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Pri_Interest";
        #endregion

        #region 构造函数

        public Interest()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 点价申请序号
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
        /// 币种
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
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
        /// 预付本金
        /// </summary>
        public decimal PayCapital
        {
            get { return payCapital; }
            set { payCapital = value; }
        }

        /// <summary>
        /// 当前计息本金
        /// </summary>
        public decimal CurCapital
        {
            get { return curCapital; }
            set { curCapital = value; }
        }

        /// <summary>
        /// 税率
        /// </summary>
        public decimal InterestRate
        {
            get { return interestRate; }
            set { interestRate = value; }
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
        /// v
        /// </summary>
        public decimal InterestAmountDay
        {
            get { return interestAmountDay; }
            set { interestAmountDay = value; }
        }

        /// <summary>
        /// 结息重量
        /// </summary>
        public decimal InterestAmount
        {
            get { return interestAmount; }
            set { interestAmount = value; }
        }

        /// <summary>
        /// 重量单位
        /// </summary>
        public int Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        /// <summary>
        /// 计息方式
        /// </summary>
        public int InterestStyle
        {
            get { return interestStyle; }
            set { interestStyle = value; }
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
        /// 结息日期
        /// </summary>
        public DateTime InterestDate
        {
            get { return interestDate; }
            set { interestDate = value; }
        }

        /// <summary>
        /// 利息结算状态
        /// </summary>
        public StatusEnum InterestStatus
        {
            get { return interestStatus; }
            set { interestStatus = value; }
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
            get { return this.interestId; }
            set { this.interestId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return interestStatus; }
            set { interestStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.DoPrice.DAL.InterestDAL";
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