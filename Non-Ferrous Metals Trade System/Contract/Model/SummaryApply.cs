
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SummaryApply.cs
// 文件功能描述：制单指令dbo.Con_SummaryApply实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Contract.Model
{
    /// <summary>
    /// 制单指令dbo.Con_SummaryApply实体类。
    /// </summary>
    [Serializable]
    public class SummaryApply : IModel
    {
        #region 字段

        private int summaryApplyId;
        private int contractId;
        private int subId;
        private int sAType;
        private DateTime sADate;
        private int applyDept;
        private int buyerCorp;
        private string buyerAddress = String.Empty;
        private int paymentStyle;
        private int recBankId;
        private int outPricOption;
        private int brandId;
        private int areaId;
        private string bankCode = String.Empty;
        private decimal grossAmount;
        private decimal netAmount;
        private int curency;
        private decimal price;
        private decimal bala;
        private string meno = String.Empty;
        private int sAStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Con_SummaryApply";

        #endregion

        #region 构造函数

        public SummaryApply()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 制单指令序号
        /// </summary>
        public int SummaryApplyId
        {
            get { return summaryApplyId; }
            set { summaryApplyId = value; }
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
        /// 制单指令类型
        /// </summary>
        public int SAType
        {
            get { return sAType; }
            set { sAType = value; }
        }

        /// <summary>
        /// 指令日期
        /// </summary>
        public DateTime SADate
        {
            get { return sADate; }
            set { sADate = value; }
        }

        /// <summary>
        /// 申请部门
        /// </summary>
        public int ApplyDept
        {
            get { return applyDept; }
            set { applyDept = value; }
        }

        /// <summary>
        /// 买家公司
        /// </summary>
        public int BuyerCorp
        {
            get { return buyerCorp; }
            set { buyerCorp = value; }
        }

        /// <summary>
        /// 买家地址
        /// </summary>
        public string BuyerAddress
        {
            get { return buyerAddress; }
            set { buyerAddress = value; }
        }

        /// <summary>
        /// 收款方式
        /// </summary>
        public int PaymentStyle
        {
            get { return paymentStyle; }
            set { paymentStyle = value; }
        }

        /// <summary>
        /// 收款银行
        /// </summary>
        public int RecBankId
        {
            get { return recBankId; }
            set { recBankId = value; }
        }

        /// <summary>
        /// 价外条款
        /// </summary>
        public int OutPricOption
        {
            get { return outPricOption; }
            set { outPricOption = value; }
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
        /// 产地
        /// </summary>
        public int AreaId
        {
            get { return areaId; }
            set { areaId = value; }
        }

        /// <summary>
        /// 银行编写
        /// </summary>
        public string BankCode
        {
            get { return bankCode; }
            set { bankCode = value; }
        }

        /// <summary>
        /// 毛重
        /// </summary>
        public decimal GrossAmount
        {
            get { return grossAmount; }
            set { grossAmount = value; }
        }

        /// <summary>
        /// 净重
        /// </summary>
        public decimal NetAmount
        {
            get { return netAmount; }
            set { netAmount = value; }
        }

        /// <summary>
        /// 币种
        /// </summary>
        public int Curency
        {
            get { return curency; }
            set { curency = value; }
        }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        /// <summary>
        /// 发票总价
        /// </summary>
        public decimal Bala
        {
            get { return bala; }
            set { bala = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Meno
        {
            get { return meno; }
            set { meno = value; }
        }

        /// <summary>
        /// 指令状态
        /// </summary>
        public int SAStatus
        {
            get { return sAStatus; }
            set { sAStatus = value; }
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
            get { return this.summaryApplyId; }
            set { this.summaryApplyId = value; }
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

        private string dalName = "NFMT.Contract.DAL.SummaryApplyDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Contract";
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