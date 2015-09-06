
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInAttach.cs
// 文件功能描述：收款附件dbo.Fun_CashInAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年10月10日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 收款附件dbo.Fun_CashInAttach实体类。
    /// </summary>
    [Serializable]
    public class CashInAttach : IModel
    {
        #region 字段

        private int cashInAttachId;
        private int attachId;
        private int cashInId;
        private string tableName = "dbo.Fun_CashInAttach";
        #endregion

        #region 构造函数

        public CashInAttach()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 收款登记附件序号
        /// </summary>
        public int CashInAttachId
        {
            get { return cashInAttachId; }
            set { cashInAttachId = value; }
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
        /// 收款登记序号
        /// </summary>
        public int CashInId
        {
            get { return cashInId; }
            set { cashInId = value; }
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
            get { return this.cashInAttachId; }
            set { this.cashInAttachId = value; }
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

        private string dalName = "NFMT.Funds.DAL.CashInAttachDAL";
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