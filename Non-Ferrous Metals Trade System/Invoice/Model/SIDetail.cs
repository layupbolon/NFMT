
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SIDetail.cs
// 文件功能描述：价外票明细dbo.Inv_SIDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月1日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Invoice.Model
{
    /// <summary>
    /// 价外票明细dbo.Inv_SIDetail实体类。
    /// </summary>
    [Serializable]
    public class SIDetail : IModel
    {
        #region 字段

        private int sIDetailId;
        private int sIId;
        private int payDept;
        private int feeType;
        private int stockId;
        private int stockLogId;
        private int contractId;
        private int contractSubId;
        private decimal detailBala;
        private Common.StatusEnum detailStatus;
        private string memo = String.Empty;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Inv_SIDetail";
        #endregion

        #region 构造函数

        public SIDetail()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 明细序号
        /// </summary>
        public int SIDetailId
        {
            get { return sIDetailId; }
            set { sIDetailId = value; }
        }

        /// <summary>
        /// 价外票序号
        /// </summary>
        public int SIId
        {
            get { return sIId; }
            set { sIId = value; }
        }

        /// <summary>
        /// 成本部门
        /// </summary>
        public int PayDept
        {
            get { return payDept; }
            set { payDept = value; }
        }

        /// <summary>
        /// 发票内容
        /// </summary>
        public int FeeType
        {
            get { return feeType; }
            set { feeType = value; }
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
        /// 采购合约序号
        /// </summary>
        public int ContractId
        {
            get { return contractId; }
            set { contractId = value; }
        }

        /// <summary>
        /// 采购子合约序号
        /// </summary>
        public int ContractSubId
        {
            get { return contractSubId; }
            set { contractSubId = value; }
        }

        /// <summary>
        /// 明细金额
        /// </summary>
        public decimal DetailBala
        {
            get { return detailBala; }
            set { detailBala = value; }
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
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        /// <summary>
        /// 创建人序号
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
            get { return this.sIDetailId; }
            set { this.sIDetailId = value; }
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

        private string dalName = "NFMT.Invoice.DAL.SIDetailDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Invoice";
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