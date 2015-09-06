
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApply.cs
// 文件功能描述：融资头寸质押申请单dbo.Fin_PledgeApply实体类。
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Finance.Model
{
    /// <summary>
    /// 融资头寸质押申请单dbo.Fin_PledgeApply实体类。
    /// </summary>
    [Serializable]
    public class PledgeApply : IModel
    {
        #region 字段

        private int pledgeApplyId;
        private string pledgeApplyNo = String.Empty;
        private int deptId;
        private DateTime applyTime;
        private int financingBankId;
        private int financingAccountId;
        private int assetId;
        private bool switchBack;
        private int exchangeId;
        private decimal sumNetAmount;
        private int sumHands;
        private Common.StatusEnum pledgeApplyStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Fin_PledgeApply";
        #endregion

        #region 构造函数

        public PledgeApply()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public int PledgeApplyId
        {
            get { return pledgeApplyId; }
            set { pledgeApplyId = value; }
        }

        /// <summary>
        /// 质押申请单号
        /// </summary>
        public string PledgeApplyNo
        {
            get { return pledgeApplyNo; }
            set { pledgeApplyNo = value; }
        }

        /// <summary>
        /// 部门序号
        /// </summary>
        public int DeptId
        {
            get { return deptId; }
            set { deptId = value; }
        }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime ApplyTime
        {
            get { return applyTime; }
            set { applyTime = value; }
        }

        /// <summary>
        /// 融资银行
        /// </summary>
        public int FinancingBankId
        {
            get { return financingBankId; }
            set { financingBankId = value; }
        }

        /// <summary>
        /// 融资银行账号序号
        /// </summary>
        public int FinancingAccountId
        {
            get { return financingAccountId; }
            set { financingAccountId = value; }
        }

        /// <summary>
        /// 融资货物序号
        /// </summary>
        public int AssetId
        {
            get { return assetId; }
            set { assetId = value; }
        }

        /// <summary>
        /// 头寸是否转回
        /// </summary>
        public bool SwitchBack
        {
            get { return switchBack; }
            set { switchBack = value; }
        }

        /// <summary>
        /// 交易所
        /// </summary>
        public int ExchangeId
        {
            get { return exchangeId; }
            set { exchangeId = value; }
        }

        /// <summary>
        /// 净重合计
        /// </summary>
        public decimal SumNetAmount
        {
            get { return sumNetAmount; }
            set { sumNetAmount = value; }
        }

        /// <summary>
        /// 手数合计
        /// </summary>
        public int SumHands
        {
            get { return sumHands; }
            set { sumHands = value; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public Common.StatusEnum PledgeApplyStatus
        {
            get { return pledgeApplyStatus; }
            set { pledgeApplyStatus = value; }
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
            get { return this.pledgeApplyId; }
            set { this.pledgeApplyId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return pledgeApplyStatus; }
            set { pledgeApplyStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Finance.DAL.PledgeApplyDAL";
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