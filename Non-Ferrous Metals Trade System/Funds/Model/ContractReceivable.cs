
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractReceivable.cs
// 文件功能描述：收款分配至合约dbo.Fun_ContractReceivable_Ref实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 收款分配至合约dbo.Fun_ContractReceivable_Ref实体类。
    /// </summary>
    [Serializable]
    public class ContractReceivable : IModel
    {
        #region 字段

        private int refId;
        private int corpRefId;
        private int allotId;
        private int detailId;
        private int recId;
        private int contractId;
        private int subContractId;
        private string tableName = "dbo.Fun_ContractReceivable_Ref";
        #endregion

        #region 构造函数

        public ContractReceivable()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 合约收款序号
        /// </summary>
        public int RefId
        {
            get { return refId; }
            set { refId = value; }
        }

        /// <summary>
        /// 收款分配公司序号
        /// </summary>
        public int CorpRefId
        {
            get { return corpRefId; }
            set { corpRefId = value; }
        }

        /// <summary>
        /// 分配序号
        /// </summary>
        public int AllotId
        {
            get { return allotId; }
            set { allotId = value; }
        }

        /// <summary>
        /// 明细序号
        /// </summary>
        public int DetailId
        {
            get { return detailId; }
            set { detailId = value; }
        }

        /// <summary>
        /// 收款登记序号
        /// </summary>
        public int RecId
        {
            get { return recId; }
            set { recId = value; }
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
        /// 子合约序号
        /// </summary>
        public int SubContractId
        {
            get { return subContractId; }
            set { subContractId = value; }
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
            get { return this.refId; }
            set { this.refId = value; }
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

        private string dalName = "NFMT.Funds.DAL.ContractReceivableDAL";
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