
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SubCorporationDetail.cs
// 文件功能描述：子合约抬头明细dbo.Con_SubCorporationDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月5日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Contract.Model
{
    /// <summary>
    /// 子合约抬头明细dbo.Con_SubCorporationDetail实体类。
    /// </summary>
    [Serializable]
    public class SubCorporationDetail : IModel
    {
        #region 字段

        private int detailId;
        private int contractId;
        private int subId;
        private int corpId;
        private string corpName = String.Empty;
        private int deptId;
        private string deptName = String.Empty;
        private StatusEnum detailStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private bool isDefaultCorp;
        private bool isInnerCorp;
        private string tableName = "dbo.Con_SubCorporationDetail";
        #endregion

        #region 构造函数

        public SubCorporationDetail()
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
        /// 子合约序号
        /// </summary>
        public int SubId
        {
            get { return subId; }
            set { subId = value; }
        }

        /// <summary>
        /// 公司序号
        /// </summary>
        public int CorpId
        {
            get { return corpId; }
            set { corpId = value; }
        }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CorpName
        {
            get { return corpName; }
            set { corpName = value; }
        }

        /// <summary>
        /// 成本中心序号
        /// </summary>
        public int DeptId
        {
            get { return deptId; }
            set { deptId = value; }
        }

        /// <summary>
        /// 成本中心名称
        /// </summary>
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }

        /// <summary>
        /// 抬头状态
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
        /// 是否默认公司
        /// </summary>
        public bool IsDefaultCorp
        {
            get { return isDefaultCorp; }
            set { isDefaultCorp = value; }
        }

        /// <summary>
        /// 是否内部公司
        /// </summary>
        public bool IsInnerCorp
        {
            get { return isInnerCorp; }
            set { isInnerCorp = value; }
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

        private string dalName = "NFMT.Contract.DAL.SubCorporationDetailDAL";
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