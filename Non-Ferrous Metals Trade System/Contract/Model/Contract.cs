
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Contract.cs
// 文件功能描述：合约dbo.Con_Contract实体类。
// 创建人：CodeSmith
// 创建时间： 2015年1月20日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Contract.Model
{
    /// <summary>
    /// 合约dbo.Con_Contract实体类。
    /// </summary>
    [Serializable]
    public class Contract : IModel
    {
        #region 字段

        private int contractId;
        private string contractNo = String.Empty;
        private string outContractNo = String.Empty;
        private DateTime contractDate;
        private string contractName = String.Empty;
        private int tradeBorder;
        private int contractLimit;
        private int contractSide;
        private int tradeDirection;
        private int haveGoodsFlow;
        private DateTime contractPeriodS;
        private DateTime contractPeriodE;
        private decimal premium;
        private DateTime? initQP;
        private int assetId;
        private int settleCurrency;
        private decimal signAmount;
        private decimal exeAmount;
        private int unitId;
        private int priceMode;
        private string memo = String.Empty;
        private int deliveryStyle;
        private DateTime? deliveryDate;
        private int createFrom;
        private StatusEnum contractStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Con_Contract";
        #endregion

        #region 构造函数

        public Contract()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 合约序号
        /// </summary>
        public int ContractId
        {
            get { return contractId; }
            set { contractId = value; }
        }

        /// <summary>
        /// 内部合约号
        /// </summary>
        public string ContractNo
        {
            get { return contractNo; }
            set { contractNo = value; }
        }

        /// <summary>
        /// 外部合约号
        /// </summary>
        public string OutContractNo
        {
            get { return outContractNo; }
            set { outContractNo = value; }
        }

        /// <summary>
        /// 签订日期
        /// </summary>
        public DateTime ContractDate
        {
            get { return contractDate; }
            set { contractDate = value; }
        }

        /// <summary>
        /// 合约名称
        /// </summary>
        public string ContractName
        {
            get { return contractName; }
            set { contractName = value; }
        }

        /// <summary>
        /// 贸易境区(内外贸)
        /// </summary>
        public int TradeBorder
        {
            get { return tradeBorder; }
            set { tradeBorder = value; }
        }

        /// <summary>
        /// 合同时限(长零)
        /// </summary>
        public int ContractLimit
        {
            get { return contractLimit; }
            set { contractLimit = value; }
        }

        /// <summary>
        /// 合同对方(内外部合同)
        /// </summary>
        public int ContractSide
        {
            get { return contractSide; }
            set { contractSide = value; }
        }

        /// <summary>
        /// 贸易方向(购销)
        /// </summary>
        public int TradeDirection
        {
            get { return tradeDirection; }
            set { tradeDirection = value; }
        }

        /// <summary>
        /// 贸易融资
        /// </summary>
        public int HaveGoodsFlow
        {
            get { return haveGoodsFlow; }
            set { haveGoodsFlow = value; }
        }

        /// <summary>
        /// 开始执行日
        /// </summary>
        public DateTime ContractPeriodS
        {
            get { return contractPeriodS; }
            set { contractPeriodS = value; }
        }

        /// <summary>
        /// 结束执行日
        /// </summary>
        public DateTime ContractPeriodE
        {
            get { return contractPeriodE; }
            set { contractPeriodE = value; }
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
        /// 初始QP
        /// </summary>
        public DateTime? InitQP
        {
            get { return initQP; }
            set { initQP = value; }
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
        /// 结算币种
        /// </summary>
        public int SettleCurrency
        {
            get { return settleCurrency; }
            set { settleCurrency = value; }
        }

        /// <summary>
        /// 签订数量
        /// </summary>
        public decimal SignAmount
        {
            get { return signAmount; }
            set { signAmount = value; }
        }

        /// <summary>
        /// 执行数量
        /// </summary>
        public decimal ExeAmount
        {
            get { return exeAmount; }
            set { exeAmount = value; }
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
        /// 定价方式
        /// </summary>
        public int PriceMode
        {
            get { return priceMode; }
            set { priceMode = value; }
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
        /// 交货方式
        /// </summary>
        public int DeliveryStyle
        {
            get { return deliveryStyle; }
            set { deliveryStyle = value; }
        }

        /// <summary>
        /// 交货日期
        /// </summary>
        public DateTime? DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; }
        }

        /// <summary>
        /// 创建来源
        /// </summary>
        public int CreateFrom
        {
            get { return createFrom; }
            set { createFrom = value; }
        }

        /// <summary>
        /// 合同状态
        /// </summary>
        public StatusEnum ContractStatus
        {
            get { return contractStatus; }
            set { contractStatus = value; }
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
            get { return this.contractId; }
            set { this.contractId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return contractStatus; }
            set { contractStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Contract.DAL.ContractDAL";
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