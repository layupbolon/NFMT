
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractAttach.cs
// 文件功能描述：合约附件dbo.Con_ContractAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Contract.Model
{
    /// <summary>
    /// 合约附件dbo.Con_ContractAttach实体类。
    /// </summary>
    [Serializable]
    public class ContractAttach : IAttachModel
    {
        #region 字段

        private int contractAttachId;
        private int contractId;
        private int attachId;
        private string tableName = "dbo.Con_ContractAttach";

        #endregion

        #region 构造函数

        public ContractAttach()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 合约附件序号
        /// </summary>
        public int ContractAttachId
        {
            get { return contractAttachId; }
            set { contractAttachId = value; }
        }

        /// <summary>
        /// 合约序号
        /// </summary>
        public int ContractId
        {
            get { return contractId; }
            set { contractId = value; }
        }

        /// <summary>
        /// 附件主表序号
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
            get { return this.contractAttachId; }
            set { this.contractAttachId = value; }
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

        private string dalName = "NFMT.Contract.DAL.ContractAttachDAL";
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


        #region 实现接口

        public int BussinessDataId
        {
            get { return contractId; }
            set { contractId = value; }
        }

        #endregion
    }
}