
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockPayApply.cs
// 文件功能描述：库存付款申请dbo.Fun_StockPayApply_Ref实体类。
// 创建人：CodeSmith
// 创建时间： 2014年10月10日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 库存付款申请dbo.Fun_StockPayApply_Ref实体类。
    /// </summary>
    [Serializable]
    public class StockPayApply : IModel
    {
        #region 字段

        private int refId;
        private int payApplyId;
        private int contractRefId;
        private int stockId;
        private int stockLogId;
        private int contractId;
        private int subId;
        private decimal applyBala;
        private StatusEnum refStatus;
        private string tableName = "dbo.Fun_StockPayApply_Ref";
        #endregion

        #region 构造函数

        public StockPayApply()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 库存付款申请序号
        /// </summary>
        public int RefId
        {
            get { return refId; }
            set { refId = value; }
        }

        /// <summary>
        /// 付款申请序号
        /// </summary>
        public int PayApplyId
        {
            get { return payApplyId; }
            set { payApplyId = value; }
        }

        /// <summary>
        /// 合约申请序号
        /// </summary>
        public int ContractRefId
        {
            get { return contractRefId; }
            set { contractRefId = value; }
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
        /// 申请金额
        /// </summary>
        public decimal ApplyBala
        {
            get { return applyBala; }
            set { applyBala = value; }
        }

        /// <summary>
        /// 明细状态
        /// </summary>
        public StatusEnum RefStatus
        {
            get { return refStatus; }
            set { refStatus = value; }
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
            get { return this.refId; }
            set { this.refId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return refStatus; }
            set { refStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Funds.DAL.StockPayApplyDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Funds";
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