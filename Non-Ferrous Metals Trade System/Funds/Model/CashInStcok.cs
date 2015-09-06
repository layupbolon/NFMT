﻿
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInStcok.cs
// 文件功能描述：收款分配至库存dbo.Fun_CashInStcok_Ref实体类。
// 创建人：CodeSmith
// 创建时间： 2015年3月19日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 收款分配至库存dbo.Fun_CashInStcok_Ref实体类。
    /// </summary>
    [Serializable]
    public class CashInStcok : IModel
    {
        #region 字段

        private int refId;
        private int allotId;
        private int corpRefId;
        private int contractRefId;
        private int cashInId;
        private int stockId;
        private int stockLogId;
        private int stockNameId;
        private decimal allotNetAmount;
        private decimal allotBala;
        private Common.StatusEnum detailStatus;
        private int fundsLogId;
        private string tableName = "dbo.Fun_CashInStcok_Ref";
        #endregion

        #region 构造函数

        public CashInStcok()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public int RefId
        {
            get { return refId; }
            set { refId = value; }
        }

        /// <summary>
        /// 分配序号
        /// </summary>
        public int AllotId
        {
            get { return allotId; }
            set { allotId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CorpRefId
        {
            get { return corpRefId; }
            set { corpRefId = value; }
        }

        /// <summary>
        /// 收款分配合约序号
        /// </summary>
        public int ContractRefId
        {
            get { return contractRefId; }
            set { contractRefId = value; }
        }

        /// <summary>
        /// 收款登记序号
        /// </summary>
        public int CashInId
        {
            get { return cashInId; }
            set { cashInId = value; }
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
        /// 库存流水
        /// </summary>
        public int StockLogId
        {
            get { return stockLogId; }
            set { stockLogId = value; }
        }

        /// <summary>
        /// 业务单号
        /// </summary>
        public int StockNameId
        {
            get { return stockNameId; }
            set { stockNameId = value; }
        }

        /// <summary>
        /// 配款净重
        /// </summary>
        public decimal AllotNetAmount
        {
            get { return allotNetAmount; }
            set { allotNetAmount = value; }
        }

        /// <summary>
        /// 分配金额
        /// </summary>
        public decimal AllotBala
        {
            get { return allotBala; }
            set { allotBala = value; }
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
        /// 资金流水序号
        /// </summary>
        public int FundsLogId
        {
            get { return fundsLogId; }
            set { fundsLogId = value; }
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

        private string dalName = "NFMT.Funds.DAL.CashInStcokDAL";
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