
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractPayApply.cs
// 文件功能描述：合约付款申请dbo.Fun_ContractPayApply_Ref实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月12日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 合约付款申请dbo.Fun_ContractPayApply_Ref实体类。
    /// </summary>
    [Serializable]
    public class ContractPayApply : IModel
    {
        #region 字段

        private int refId;
        private int payApplyId;
        private int contractId;
        private int contractSubId;
        private decimal applyBala;
        private string tableName = "dbo.Fun_ContractPayApply_Ref";
        #endregion

        #region 构造函数

        public ContractPayApply()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 合约付款申请序号
        /// </summary>
        public int RefId
        {
            get { return refId; }
            set { refId = value; }
        }

        /// <summary>
        /// 付款申请序号
        /// </summary>
        public int PayApplyId
        {
            get { return payApplyId; }
            set { payApplyId = value; }
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
        public int ContractSubId
        {
            get { return contractSubId; }
            set { contractSubId = value; }
        }

        /// <summary>
        /// 申请金额
        /// </summary>
        public decimal ApplyBala
        {
            get { return applyBala; }
            set { applyBala = value; }
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

        private string dalName = "NFMT.Funds.DAL.ContractPayApplyDAL";
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