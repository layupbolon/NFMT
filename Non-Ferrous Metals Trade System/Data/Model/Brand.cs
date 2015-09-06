
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Brand.cs
// 文件功能描述：dbo.Brand实体类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
    /// <summary>
    /// dbo.Brand实体类。
    /// </summary>
    [Serializable]
    public class Brand : IModel
    {
        #region 字段

        private int brandId;
        private int producerId;
        private string brandName = String.Empty;
        private string brandFullName = String.Empty;
        private string brandShort = String.Empty;
        //private int brandType;
        private string brandInfo = String.Empty;
        private Common.StatusEnum brandStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Brand";

        #endregion

        #region 构造函数

        public Brand()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int BrandId
        {
            get { return brandId; }
            set { brandId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ProducerId
        {
            get { return producerId; }
            set { producerId = value; }
        }
        /// <summary>
        /// 生产商名称
        /// </summary>
        public string ProducerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BrandName
        {
            get { return brandName; }
            set { brandName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BrandFullName
        {
            get { return brandFullName; }
            set { brandFullName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        //public string BrandShort
        //{
        //    get { return brandShort; }
        //    set { brandShort = value; }
        //}

        /// <summary>
        /// 
        /// </summary>
        //public int BrandType
        //{
        //    get { return brandType; }
        //    set { brandType = value; }
        //}

        /// <summary>
        /// 
        /// </summary>
        public string BrandInfo
        {
            get { return brandInfo; }
            set { brandInfo = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum BrandStatus
        {
            get { return brandStatus; }
            set { brandStatus = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CreatorId
        {
            get { return creatorId; }
            set { creatorId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int LastModifyId
        {
            get { return lastModifyId; }
            set { lastModifyId = value; }
        }

        /// <summary>
        /// 
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
            get { return this.brandId; }
            set { this.brandId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return brandStatus; }
            set { brandStatus = value; }
        }
        /// <summary>
        /// 数据状态名
        /// </summary>
        public string BrandStatusName
        {
            get { return this.brandStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.BrandDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Data";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT_Basic";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}