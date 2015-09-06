
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Invoice.cs
// 文件功能描述：发票dbo.Invoice实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月1日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Invoice.Model
{
    /// <summary>
    /// 发票dbo.Invoice实体类。
    /// </summary>
    [Serializable]
    public class Invoice : IModel
    {
        #region 字段

        private int invoiceId;
        private DateTime invoiceDate;
        private string invoiceNo = String.Empty;
        private string invoiceName = String.Empty;
        private InvoiceTypeEnum invioceType;
        private decimal invoiceBala;
        private int currencyId;
        private int invoiceDirection;
        private int outBlocId;
        private int outCorpId;
        private string outCorpName = String.Empty;
        private int inBlocId;
        private int inCorpId;
        private string inCorpName = String.Empty;
        private Common.StatusEnum invoiceStatus;
        private string memo = String.Empty;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Invoice";
        #endregion

        #region 构造函数

        public Invoice()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 发票序号
        /// </summary>
        public int InvoiceId
        {
            get { return invoiceId; }
            set { invoiceId = value; }
        }

        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime InvoiceDate
        {
            get { return invoiceDate; }
            set { invoiceDate = value; }
        }

        /// <summary>
        /// 发票编号
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }

        /// <summary>
        /// 实际票据号
        /// </summary>
        public string InvoiceName
        {
            get { return invoiceName; }
            set { invoiceName = value; }
        }

        /// <summary>
        /// 发票类型
        /// </summary>
        public InvoiceTypeEnum InvoiceType
        {
            get { return invioceType; }
            set { invioceType = value; }
        }

        /// <summary>
        /// 发票金额
        /// </summary>
        public decimal InvoiceBala
        {
            get { return invoiceBala; }
            set { invoiceBala = value; }
        }

        /// <summary>
        /// 发票币种
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }

        /// <summary>
        /// 发票方向(开具/收取)
        /// </summary>
        public int InvoiceDirection
        {
            get { return invoiceDirection; }
            set { invoiceDirection = value; }
        }

        /// <summary>
        /// 开票集团
        /// </summary>
        public int OutBlocId
        {
            get { return outBlocId; }
            set { outBlocId = value; }
        }

        /// <summary>
        /// 开票公司
        /// </summary>
        public int OutCorpId
        {
            get { return outCorpId; }
            set { outCorpId = value; }
        }

        /// <summary>
        /// 开票公司名称
        /// </summary>
        public string OutCorpName
        {
            get { return outCorpName; }
            set { outCorpName = value; }
        }

        /// <summary>
        /// 收票集团
        /// </summary>
        public int InBlocId
        {
            get { return inBlocId; }
            set { inBlocId = value; }
        }

        /// <summary>
        /// 收票公司
        /// </summary>
        public int InCorpId
        {
            get { return inCorpId; }
            set { inCorpId = value; }
        }

        /// <summary>
        /// 收票公司名称
        /// </summary>
        public string InCorpName
        {
            get { return inCorpName; }
            set { inCorpName = value; }
        }

        /// <summary>
        /// 发票状态
        /// </summary>
        public Common.StatusEnum InvoiceStatus
        {
            get { return invoiceStatus; }
            set { invoiceStatus = value; }
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
            get { return this.invoiceId; }
            set { this.invoiceId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return invoiceStatus; }
            set { invoiceStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Invoice.DAL.InvoiceDAL";
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