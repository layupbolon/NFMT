
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentOrder.cs
// 文件功能描述：制单指令dbo.Doc_DocumentOrder实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Document.Model
{
    /// <summary>
    /// 制单指令dbo.Doc_DocumentOrder实体类。
    /// </summary>
    [Serializable]
    public class DocumentOrder : IModel
    {
        #region 字段

        private int orderId;
        private string orderNo = String.Empty;
        private int commercialId;
        private int contractId;
        private string contractNo = String.Empty;
        private int subId;
        private int lCId;
        private string lCNo = String.Empty;
        private int lCDay;
        private int orderType;
        private DateTime orderDate;
        private int applyCorp;
        private int applyDept;
        private int sellerCorp;
        private int buyerCorp;
        private string buyerCorpName = String.Empty;
        private string buyerAddress = String.Empty;
        private int paymentStyle;
        private int recBankId;
        private string discountBase;
        private int assetId;
        private int brandId;
        private int areaId;
        private string areaName = String.Empty;
        private string bankCode = String.Empty;
        private decimal grossAmount;
        private decimal netAmount;
        private int unitId;
        private int currency;
        private decimal unitPrice;
        private decimal invBala;
        private decimal invGap;
        private int bundles;
        private string meno = String.Empty;
        private StatusEnum orderStatus;
        private int applyEmpId;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Doc_DocumentOrder";
        #endregion

        #region 构造函数

        public DocumentOrder()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 制单指令序号
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        /// <summary>
        /// 批次号
        /// </summary>
        public string OrderNo
        {
            get { return orderNo; }
            set { orderNo = value; }
        }

        /// <summary>
        /// 临票指令序号
        /// </summary>
        public int CommercialId
        {
            get { return commercialId; }
            set { commercialId = value; }
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
        /// 合约号
        /// </summary>
        public string ContractNo
        {
            get { return contractNo; }
            set { contractNo = value; }
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
        /// LC序号
        /// </summary>
        public int LCId
        {
            get { return lCId; }
            set { lCId = value; }
        }

        /// <summary>
        /// LC编号
        /// </summary>
        public string LCNo
        {
            get { return lCNo; }
            set { lCNo = value; }
        }

        /// <summary>
        /// LC天数
        /// </summary>
        public int LCDay
        {
            get { return lCDay; }
            set { lCDay = value; }
        }

        /// <summary>
        /// 制单指令类型
        /// </summary>
        public int OrderType
        {
            get { return orderType; }
            set { orderType = value; }
        }

        /// <summary>
        /// 指令日期
        /// </summary>
        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }

        /// <summary>
        /// 申请公司
        /// </summary>
        public int ApplyCorp
        {
            get { return applyCorp; }
            set { applyCorp = value; }
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
        /// 我方公司
        /// </summary>
        public int SellerCorp
        {
            get { return sellerCorp; }
            set { sellerCorp = value; }
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
        /// 买家公司名称
        /// </summary>
        public string BuyerCorpName
        {
            get { return buyerCorpName; }
            set { buyerCorpName = value; }
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
        /// 价格条款
        /// </summary>
        public string DiscountBase
        {
            get { return discountBase; }
            set { discountBase = value; }
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
        /// 原产地
        /// </summary>
        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
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
        /// 单位
        /// </summary>
        public int UnitId
        {
            get { return unitId; }
            set { unitId = value; }
        }

        /// <summary>
        /// 币种
        /// </summary>
        public int Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        /// <summary>
        /// 发票总价
        /// </summary>
        public decimal InvBala
        {
            get { return invBala; }
            set { invBala = value; }
        }

        /// <summary>
        /// 差额
        /// </summary>
        public decimal InvGap
        {
            get { return invGap; }
            set { invGap = value; }
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
        public StatusEnum OrderStatus
        {
            get { return orderStatus; }
            set { orderStatus = value; }
        }

        /// <summary>
        /// 申请人
        /// </summary>
        public int ApplyEmpId
        {
            get { return applyEmpId; }
            set { applyEmpId = value; }
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
            get { return this.orderId; }
            set { this.orderId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return orderStatus; }
            set { orderStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Document.DAL.DocumentOrderDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Document";
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