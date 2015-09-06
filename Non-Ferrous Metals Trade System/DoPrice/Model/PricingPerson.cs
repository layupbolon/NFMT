
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingPerson.cs
// 文件功能描述：点价权限人dbo.Pri_PricingPerson实体类。
// 创建人：CodeSmith
// 创建时间： 2014年10月15日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
    /// <summary>
    /// 点价权限人dbo.Pri_PricingPerson实体类。
    /// </summary>
    [Serializable]
    public class PricingPerson : IModel
    {
        #region 字段

        private int persoinId;
        private int blocId;
        private int corpId;
        private string pricingName = String.Empty;
        private string job = String.Empty;
        private string pricingPhone = String.Empty;
        private string phone2 = String.Empty;
        private StatusEnum persoinStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Pri_PricingPerson";
        #endregion

        #region 构造函数

        public PricingPerson()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 权限人序号
        /// </summary>
        public int PersoinId
        {
            get { return persoinId; }
            set { persoinId = value; }
        }

        /// <summary>
        /// 归属集团
        /// </summary>
        public int BlocId
        {
            get { return blocId; }
            set { blocId = value; }
        }

        /// <summary>
        /// 归属公司
        /// </summary>
        public int CorpId
        {
            get { return corpId; }
            set { corpId = value; }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PricingName
        {
            get { return pricingName; }
            set { pricingName = value; }
        }

        /// <summary>
        /// 职位
        /// </summary>
        public string Job
        {
            get { return job; }
            set { job = value; }
        }

        /// <summary>
        /// 点价电话
        /// </summary>
        public string PricingPhone
        {
            get { return pricingPhone; }
            set { pricingPhone = value; }
        }

        /// <summary>
        /// 点价电话2
        /// </summary>
        public string Phone2
        {
            get { return phone2; }
            set { phone2 = value; }
        }

        /// <summary>
        /// 权限人状态
        /// </summary>
        public StatusEnum PersoinStatus
        {
            get { return persoinStatus; }
            set { persoinStatus = value; }
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
        /// 创建时间
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
            get { return this.persoinId; }
            set { this.persoinId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return persoinStatus; }
            set { persoinStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.DoPrice.DAL.PricingPersonDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.DoPrice";
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