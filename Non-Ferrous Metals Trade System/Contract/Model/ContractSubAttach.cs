
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractSubAttach.cs
// 文件功能描述：子合约附件dbo.Con_ContractSubAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Contract.Model
{
    /// <summary>
    /// 子合约附件dbo.Con_ContractSubAttach实体类。
    /// </summary>
    [Serializable]
    public class ContractSubAttach : IModel, IAttachModel
    {
        #region 字段

        private int subAttachId;
        private int subId;
        private int attachId;
        private string tableName = "dbo.Con_ContractSubAttach";

        #endregion

        #region 构造函数

        public ContractSubAttach()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 子合约附件序号
        /// </summary>
        public int SubAttachId
        {
            get { return subAttachId; }
            set { subAttachId = value; }
        }

        /// <summary>
        /// 子合约序号
        /// </summary>
        public int SubId
        {
            get { return subId; }
            set { subId = value; }
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
            get { return this.subAttachId; }
            set { this.subAttachId = value; }
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

        private string dalName = "NFMT.Contract.DAL.ContractSubAttachDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Contract";
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
            get { return subId; }
            set { subId = value; }
        }
    }
}