
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentAttach.cs
// 文件功能描述：财务付款附件dbo.Fun_PaymentAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月15日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 财务付款附件dbo.Fun_PaymentAttach实体类。
    /// </summary>
    [Serializable]
    public class PaymentAttach : IAttachModel
    {
        #region 字段

        private int paymentAttachId;
        private int attachId;
        private int paymentId;
        private string tableName = "dbo.Fun_PaymentAttach";
        #endregion

        #region 构造函数

        public PaymentAttach()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 财务付款附件序号
        /// </summary>
        public int PaymentAttachId
        {
            get { return paymentAttachId; }
            set { paymentAttachId = value; }
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
        /// 财务付款序号
        /// </summary>
        public int PaymentId
        {
            get { return paymentId; }
            set { paymentId = value; }
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
            get { return this.paymentAttachId; }
            set { this.paymentAttachId = value; }
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

        private string dalName = "NFMT.Funds.DAL.PaymentAttachDAL";
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
            get { return paymentId; }
            set { paymentId = value; }
        }

        #endregion
    }
}