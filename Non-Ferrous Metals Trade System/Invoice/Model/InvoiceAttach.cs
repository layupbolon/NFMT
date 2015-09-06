
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceAttach.cs
// 文件功能描述：发票附件dbo.InvoiceAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月25日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Invoice.Model
{
    /// <summary>
    /// 发票附件dbo.InvoiceAttach实体类。
    /// </summary>
    [Serializable]
    public class InvoiceAttach : IAttachModel
    {
        #region 字段

        private int invoiceAttachId;
        private int attachId;
        private int invoiceId;
        private string tableName = "dbo.InvoiceAttach";
        #endregion

        #region 构造函数

        public InvoiceAttach()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 发票附件序号
        /// </summary>
        public int InvoiceAttachId
        {
            get { return invoiceAttachId; }
            set { invoiceAttachId = value; }
        }

        /// <summary>
        /// 附件序号
        /// </summary>
        public int AttachId
        {
            get { return attachId; }
            set { attachId = value; }
        }

        /// <summary>
        /// 发票序号
        /// </summary>
        public int InvoiceId
        {
            get { return invoiceId; }
            set { invoiceId = value; }
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
            get { return this.invoiceAttachId; }
            set { this.invoiceAttachId = value; }
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

        private string dalName = "NFMT.Funds.DAL.InvoiceAttachDAL";
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

        #region 实现接口

        public int BussinessDataId
        {
            get { return invoiceId; }
            set { invoiceId = value; }
        }

        #endregion
    }
}