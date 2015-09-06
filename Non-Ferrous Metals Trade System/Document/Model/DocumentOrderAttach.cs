
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DocumentOrderAttach.cs
// 文件功能描述：制单指令附件dbo.Doc_DocumentOrderAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月20日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Document.Model
{
    /// <summary>
    /// 制单指令附件dbo.Doc_DocumentOrderAttach实体类。
    /// </summary>
    [Serializable]
    public class DocumentOrderAttach : IAttachModel
    {
        #region 字段

        private int orderAttachId;
        private int orderId;
        private int attachId;
        private string tableName = "dbo.Doc_DocumentOrderAttach";
        #endregion

        #region 构造函数

        public DocumentOrderAttach()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 制单指令附件序号
        /// </summary>
        public int OrderAttachId
        {
            get { return orderAttachId; }
            set { orderAttachId = value; }
        }

        /// <summary>
        /// 制单指令
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        /// <summary>
        /// 附件序号
        /// </summary>
        public int AttachId
        {
            get { return attachId; }
            set { attachId = value; }
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
            get { return this.orderAttachId; }
            set { this.orderAttachId = value; }
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

        private string dalName = "NFMT.Document.DAL.DocumentOrderAttachDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Document";
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

        public int BussinessDataId
        {
            get { return this.orderId; }
            set { this.orderId = value; }
        }
    }
}