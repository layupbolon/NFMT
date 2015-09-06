
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractSubQP.cs
// 文件功能描述：子合约QPdbo.Con_ContractSubQP实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Contract.Model
{
    /// <summary>
    /// 子合约QPdbo.Con_ContractSubQP实体类。
    /// </summary>
    [Serializable]
    public class ContractSubQP : IModel
    {
        #region 字段

        private int qPId;
        private int subId;
        private int curQP;
        private decimal initAmount;
        private decimal chgedAmount;
        private int qPChgDate;
        private int qPFrom;
        private decimal thisQPFeeBala;
        private string qPMemo = String.Empty;
        private decimal totalInitFeeBala;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Con_ContractSubQP";

        #endregion

        #region 构造函数

        public ContractSubQP()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// QP序号
        /// </summary>
        public int QPId
        {
            get { return qPId; }
            set { qPId = value; }
        }

        /// <summary>
        /// 子合约内序号
        /// </summary>
        public int SubId
        {
            get { return subId; }
            set { subId = value; }
        }

        /// <summary>
        /// 当前QP
        /// </summary>
        public int CurQP
        {
            get { return curQP; }
            set { curQP = value; }
        }

        /// <summary>
        /// 初数量
        /// </summary>
        public decimal InitAmount
        {
            get { return initAmount; }
            set { initAmount = value; }
        }

        /// <summary>
        /// 被延期数量
        /// </summary>
        public decimal ChgedAmount
        {
            get { return chgedAmount; }
            set { chgedAmount = value; }
        }

        /// <summary>
        /// 延期日
        /// </summary>
        public int QPChgDate
        {
            get { return qPChgDate; }
            set { qPChgDate = value; }
        }

        /// <summary>
        /// 前QP序号
        /// </summary>
        public int QPFrom
        {
            get { return qPFrom; }
            set { qPFrom = value; }
        }

        /// <summary>
        /// 本次延期费用金额
        /// </summary>
        public decimal ThisQPFeeBala
        {
            get { return thisQPFeeBala; }
            set { thisQPFeeBala = value; }
        }

        /// <summary>
        /// QPMemo
        /// </summary>
        public string QPMemo
        {
            get { return qPMemo; }
            set { qPMemo = value; }
        }

        /// <summary>
        /// 从初始QP至今累积金额
        /// </summary>
        public decimal TotalInitFeeBala
        {
            get { return totalInitFeeBala; }
            set { totalInitFeeBala = value; }
        }

        /// <summary>
        /// 创建人序号
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
            get { return this.qPId; }
            set { this.qPId = value; }
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

        private string dalName = "NFMT.Contract.DAL.ContractSubQPDAL";
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
    }
}