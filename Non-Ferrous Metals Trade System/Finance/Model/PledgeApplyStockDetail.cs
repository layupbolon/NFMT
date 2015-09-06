
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApplyStockDetail.cs
// 文件功能描述：质押申请单实货明细dbo.Fin_PledgeApplyStockDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Finance.Model
{
    /// <summary>
    /// 质押申请单实货明细dbo.Fin_PledgeApplyStockDetail实体类。
    /// </summary>
    [Serializable]
    public class PledgeApplyStockDetail : IModel
    {
        #region 字段

        private int stockDetailId;
        private int pledgeApplyId;
        private string contractNo = String.Empty;
        private decimal netAmount;
        private int stockId;
        private string refNo = String.Empty;
        private string deadline = String.Empty;
        private int hands;
        private string memo = String.Empty;
        private Common.StatusEnum detailStatus;
        private string tableName = "dbo.Fin_PledgeApplyStockDetail";
        #endregion

        #region 构造函数

        public PledgeApplyStockDetail()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public int StockDetailId
        {
            get { return stockDetailId; }
            set { stockDetailId = value; }
        }

        /// <summary>
        /// 质押申请单序号
        /// </summary>
        public int PledgeApplyId
        {
            get { return pledgeApplyId; }
            set { pledgeApplyId = value; }
        }

        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNo
        {
            get { return contractNo; }
            set { contractNo = value; }
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
        /// 库存序号
        /// </summary>
        public int StockId
        {
            get { return stockId; }
            set { stockId = value; }
        }

        /// <summary>
        /// 业务单号
        /// </summary>
        public string RefNo
        {
            get { return refNo; }
            set { refNo = value; }
        }

        /// <summary>
        /// 期限
        /// </summary>
        public string Deadline
        {
            get { return deadline; }
            set { deadline = value; }
        }

        /// <summary>
        /// 匹配手数
        /// </summary>
        public int Hands
        {
            get { return hands; }
            set { hands = value; }
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
        /// 状态
        /// </summary>
        public Common.StatusEnum DetailStatus
        {
            get { return detailStatus; }
            set { detailStatus = value; }
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
            get { return this.stockDetailId; }
            set { this.stockDetailId = value; }
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

        private string dalName = "NFMT.Finance.DAL.PledgeApplyStockDetailDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Finance";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "Financing";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}