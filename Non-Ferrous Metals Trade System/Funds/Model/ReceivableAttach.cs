﻿
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ReceivableAttach.cs
// 文件功能描述：收款登记附件dbo.Fun_ReceivableAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 收款登记附件dbo.Fun_ReceivableAttach实体类。
    /// </summary>
    [Serializable]
    public class ReceivableAttach : IAttachModel
    {
        #region 字段

        private int receivableAttachId;
        private int attachId;
        private int receivableId;
        private string tableName = "dbo.Fun_ReceivableAttach";
        #endregion

        #region 构造函数

        public ReceivableAttach()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 收款登记附件序号
        /// </summary>
        public int ReceivableAttachId
        {
            get { return receivableAttachId; }
            set { receivableAttachId = value; }
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
        public int ReceivableId
        {
            get { return receivableId; }
            set { receivableId = value; }
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
            get { return this.receivableAttachId; }
            set { this.receivableAttachId = value; }
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

        private string dalName = "NFMT.Funds.DAL.ReceivableAttachDAL";
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
            get { return receivableId; }
            set { receivableId = value; }
        }

        #endregion
    }
}