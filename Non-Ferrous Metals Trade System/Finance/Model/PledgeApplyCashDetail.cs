
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApplyCashDetail.cs
// 文件功能描述：质押申请单期货头寸明细dbo.Fin_PledgeApplyCashDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2015年4月27日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Finance.Model
{
    /// <summary>
    /// 质押申请单期货头寸明细dbo.Fin_PledgeApplyCashDetail实体类。
    /// </summary>
    [Serializable]
    public class PledgeApplyCashDetail : IModel
    {
        #region 字段

        private int cashDetailId;
        private int pledgeApplyId;
        private string stockContractNo = String.Empty;
        private string deadline = String.Empty;
        private int hands;
        private decimal price;
        private DateTime expiringDate;
        private string accountName = String.Empty;
        private string memo = String.Empty;
        private Common.StatusEnum detailStatus;
        private string tableName = "dbo.Fin_PledgeApplyCashDetail";
        #endregion

        #region 构造函数

        public PledgeApplyCashDetail()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public int CashDetailId
        {
            get { return cashDetailId; }
            set { cashDetailId = value; }
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
        /// 实货合同号
        /// </summary>
        public string StockContractNo
        {
            get { return stockContractNo; }
            set { stockContractNo = value; }
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
        /// 数量（手）
        /// </summary>
        public int Hands
        {
            get { return hands; }
            set { hands = value; }
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
        /// 到期日
        /// </summary>
        public DateTime ExpiringDate
        {
            get { return expiringDate; }
            set { expiringDate = value; }
        }

        /// <summary>
        /// 经济公司账户名
        /// </summary>
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; }
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
            get { return this.cashDetailId; }
            set { this.cashDetailId = value; }
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

        private string dalName = "NFMT.Finance.DAL.PledgeApplyCashDetailDAL";
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