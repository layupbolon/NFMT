﻿
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractTypeDetail.cs
// 文件功能描述：合约类型明细dbo.Con_ContractTypeDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Contract.Model
{
    /// <summary>
    /// 合约类型明细dbo.Con_ContractTypeDetail实体类。
    /// </summary>
    [Serializable]
    public class ContractTypeDetail : IModel
    {
        #region 字段

        private int detailId;
        private int contractId;
        private int contractType;
        private StatusEnum detailStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Con_ContractTypeDetail";
        #endregion

        #region 构造函数

        public ContractTypeDetail()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 明细序号
        /// </summary>
        public int DetailId
        {
            get { return detailId; }
            set { detailId = value; }
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
        /// 合约类型
        /// </summary>
        public int ContractType
        {
            get { return contractType; }
            set { contractType = value; }
        }

        /// <summary>
        /// 合约类型
        /// </summary>
        public int StyleDetailId
        {
            get { return contractType; }
            set { contractType = value; }
        }

        /// <summary>
        /// 明细状态
        /// </summary>
        public StatusEnum DetailStatus
        {
            get { return detailStatus; }
            set { detailStatus = value; }
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
            get { return this.detailId; }
            set { this.detailId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return detailStatus; }
            set { detailStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Contract.DAL.ContractTypeDetailDAL";
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