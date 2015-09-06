
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Producer.cs
// 文件功能描述：生产商dbo.Producer实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
    /// <summary>
    /// 生产商dbo.Producer实体类。
    /// </summary>
    [Serializable]
    public class Producer : IModel
    {
        #region 字段

        private int producerId;
        private string producerName = String.Empty;
        private string producerFullName = String.Empty;
        private string producerShort = String.Empty;
        private int producerArea;
        private Common.StatusEnum producerStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Producer";

        #endregion

        #region 构造函数

        public Producer()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 生产商序号
        /// </summary>
        public int ProducerId
        {
            get { return producerId; }
            set { producerId = value; }
        }

        /// <summary>
        /// 生产商名称
        /// </summary>
        public string ProducerName
        {
            get { return producerName; }
            set { producerName = value; }
        }

        /// <summary>
        /// 生产商全称
        /// </summary>
        public string ProducerFullName
        {
            get { return producerFullName; }
            set { producerFullName = value; }
        }

        /// <summary>
        /// 生产商简称
        /// </summary>
        public string ProducerShort
        {
            get { return producerShort; }
            set { producerShort = value; }
        }

        /// <summary>
        /// 生产商地区
        /// </summary>
        public int ProducerArea
        {
            get { return producerArea; }
            set { producerArea = value; }
        }

        public string ProducerAreaName { get; set; }

        /// <summary>
        /// 生产商状态
        /// </summary>
        public Common.StatusEnum ProducerStatus
        {
            get { return producerStatus; }
            set { producerStatus = value; }
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
            get { return this.producerId; }
            set { this.producerId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return producerStatus; }
            set { producerStatus = value; }
        }

        /// <summary>
        /// 数据状态名
        /// </summary>
        public string ProducerStatusName
        {
            get { return this.producerStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }


        private string dalName = "NFMT.Data.DAL.ProducerDAL";
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