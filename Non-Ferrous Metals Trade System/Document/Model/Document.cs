
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Document.cs
// 文件功能描述：制单dbo.Doc_Document实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Document.Model
{
    /// <summary>
    /// 制单dbo.Doc_Document实体类。
    /// </summary>
    [Serializable]
    public class Document : IModel
    {
        #region 字段

        private int documentId;
        private int orderId;
        private DateTime documentDate;
        private int docEmpId;
        private DateTime? presentDate;
        private int presenter;
        private DateTime? acceptanceDate;
        private int acceptancer;
        private string meno = String.Empty;
        private DocumentStatusEnum documentStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Doc_Document";
        #endregion

        #region 构造函数

        public Document()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 制单序号
        /// </summary>
        public int DocumentId
        {
            get { return documentId; }
            set { documentId = value; }
        }

        /// <summary>
        /// 制单指令序号
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime DocumentDate
        {
            get { return documentDate; }
            set { documentDate = value; }
        }

        /// <summary>
        /// 制单人
        /// </summary>
        public int DocEmpId
        {
            get { return docEmpId; }
            set { docEmpId = value; }
        }

        /// <summary>
        /// 交单时间
        /// </summary>
        public DateTime? PresentDate
        {
            get { return presentDate; }
            set { presentDate = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Presenter
        {
            get { return presenter; }
            set { presenter = value; }
        }

        /// <summary>
        /// 承兑确认时间
        /// </summary>
        public DateTime? AcceptanceDate
        {
            get { return acceptanceDate; }
            set { acceptanceDate = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Acceptancer
        {
            get { return acceptancer; }
            set { acceptancer = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Meno
        {
            get { return meno; }
            set { meno = value; }
        }

        /// <summary>
        /// 制单状态
        /// </summary>
        public DocumentStatusEnum DocumentStatus
        {
            get { return documentStatus; }
            set { documentStatus = value; }
        }

        /// <summary>
        /// 创建人
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
            get { return this.documentId; }
            set { this.documentId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return (StatusEnum)((int)documentStatus); }
            set { documentStatus = (DocumentStatusEnum)((int)value); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Document.DAL.DocumentDAL";
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
    }
}