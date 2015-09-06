
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PayApply.cs
// 文件功能描述：付款申请dbo.Fun_PayApply实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月13日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 付款申请dbo.Fun_PayApply实体类。
    /// </summary>
    [Serializable]
    public class PayApply : IModel
    {
        #region 字段

        private int payApplyId;
        private int applyId;
        private int payApplySource;
        private int recBlocId;
        private int recCorpId;
        private int recBankId;
        private int recBankAccountId;
        private string recBankAccount = String.Empty;
        private decimal applyBala;
        private int currencyId;
        private int payMode;
        private DateTime payDeadline;
        private string specialDesc = String.Empty;
        private int payMatter;
        private int realPayCorpId;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Fun_PayApply";
        #endregion

        #region 构造函数

        public PayApply()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 付款申请序号
        /// </summary>
        public int PayApplyId
        {
            get { return payApplyId; }
            set { payApplyId = value; }
        }

        /// <summary>
        /// 申请主表序号
        /// </summary>
        public int ApplyId
        {
            get { return applyId; }
            set { applyId = value; }
        }

        /// <summary>
        /// 付款申请来源
        /// </summary>
        public int PayApplySource
        {
            get { return payApplySource; }
            set { payApplySource = value; }
        }

        /// <summary>
        /// 收款集团序号
        /// </summary>
        public int RecBlocId
        {
            get { return recBlocId; }
            set { recBlocId = value; }
        }

        /// <summary>
        /// 收款公司序号
        /// </summary>
        public int RecCorpId
        {
            get { return recCorpId; }
            set { recCorpId = value; }
        }

        /// <summary>
        /// 收款公司银行序号
        /// </summary>
        public int RecBankId
        {
            get { return recBankId; }
            set { recBankId = value; }
        }

        /// <summary>
        /// 收款公司银行账号
        /// </summary>
        public int RecBankAccountId
        {
            get { return recBankAccountId; }
            set { recBankAccountId = value; }
        }

        /// <summary>
        /// 收款公司银行账号
        /// </summary>
        public string RecBankAccount
        {
            get { return recBankAccount; }
            set { recBankAccount = value; }
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
        /// 币种
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }

        /// <summary>
        /// 付款方式
        /// </summary>
        public int PayMode
        {
            get { return payMode; }
            set { payMode = value; }
        }

        /// <summary>
        /// 付款期限
        /// </summary>
        public DateTime PayDeadline
        {
            get { return payDeadline; }
            set { payDeadline = value; }
        }

        /// <summary>
        /// 特殊要求
        /// </summary>
        public string SpecialDesc
        {
            get { return specialDesc; }
            set { specialDesc = value; }
        }

        /// <summary>
        /// 付款事项
        /// </summary>
        public int PayMatter
        {
            get { return payMatter; }
            set { payMatter = value; }
        }

        /// <summary>
        /// 实际付款公司
        /// </summary>
        public int RealPayCorpId
        {
            get { return realPayCorpId; }
            set { realPayCorpId = value; }
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
            get { return this.payApplyId; }
            set { this.payApplyId = value; }
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

        private string dalName = "NFMT.Funds.DAL.PayApplyDAL";
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