
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorporationDetail.cs
// 文件功能描述：客户明细dbo.CorporationDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
    /// <summary>
    /// 客户明细dbo.CorporationDetail实体类。
    /// </summary>
    [Serializable]
    public class CorporationDetail : IModel
    {
        #region 字段

        private int detailId;
        private int corpId;
        private string businessLicenseCode = String.Empty;
        private decimal registeredCapital;
        private int currencyId;
        private DateTime registeredDate;
        private string corpProperty = String.Empty;
        private string businessScope = String.Empty;
        private string taxRegisteredCode = String.Empty;
        private string organizationCode = String.Empty;
        private bool isChildCorp;
        private string corpZip = String.Empty;
        private int corpType;
        private bool isSelf;
        private string memo = String.Empty;
        private Common.StatusEnum detailStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.CorporationDetail";
        #endregion

        #region 构造函数

        public CorporationDetail()
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
        /// 所属集团
        /// </summary>
        public int CorpId
        {
            get { return corpId; }
            set { corpId = value; }
        }

        /// <summary>
        /// 营业执照注册号
        /// </summary>
        public string BusinessLicenseCode
        {
            get { return businessLicenseCode; }
            set { businessLicenseCode = value; }
        }

        /// <summary>
        /// 注册资本
        /// </summary>
        public decimal RegisteredCapital
        {
            get { return registeredCapital; }
            set { registeredCapital = value; }
        }

        /// <summary>
        /// 币种
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisteredDate
        {
            get { return registeredDate; }
            set { registeredDate = value; }
        }

        /// <summary>
        /// 公司性质
        /// </summary>
        public string CorpProperty
        {
            get { return corpProperty; }
            set { corpProperty = value; }
        }

        /// <summary>
        /// 经营范围
        /// </summary>
        public string BusinessScope
        {
            get { return businessScope; }
            set { businessScope = value; }
        }

        /// <summary>
        /// 税务注册号
        /// </summary>
        public string TaxRegisteredCode
        {
            get { return taxRegisteredCode; }
            set { taxRegisteredCode = value; }
        }

        /// <summary>
        /// 组织机构注册号
        /// </summary>
        public string OrganizationCode
        {
            get { return organizationCode; }
            set { organizationCode = value; }
        }

        /// <summary>
        /// 是否子公司
        /// </summary>
        public bool IsChildCorp
        {
            get { return isChildCorp; }
            set { isChildCorp = value; }
        }

        /// <summary>
        /// 公司邮编
        /// </summary>
        public string CorpZip
        {
            get { return corpZip; }
            set { corpZip = value; }
        }

        /// <summary>
        /// 公司类型
        /// </summary>
        public int CorpType
        {
            get { return corpType; }
            set { corpType = value; }
        }

        /// <summary>
        /// 是否己方公司
        /// </summary>
        public bool IsSelf
        {
            get { return isSelf; }
            set { isSelf = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        /// <summary>
        /// 明细状态
        /// </summary>
        public Common.StatusEnum DetailStatus
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

        private string dalName = "NFMT.User.DAL.CorporationDetailDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.User";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT_User";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}