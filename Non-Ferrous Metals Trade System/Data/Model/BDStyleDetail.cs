
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BDStyleDetail.cs
// 文件功能描述：基础类型编码明细表dbo.BDStyleDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
    /// <summary>
    /// 基础类型编码明细表dbo.BDStyleDetail实体类。
    /// </summary>
    [Serializable]
    public class BDStyleDetail : IModel
    {
        #region 字段

        private int styleDetailId;
        private int bDStyleId;
        private string detailCode = String.Empty;
        private string detailName = String.Empty;
        private Common.StatusEnum detailStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.BDStyleDetail";

        #endregion

        #region 构造函数

        public BDStyleDetail()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int StyleDetailId
        {
            get { return styleDetailId; }
            set { styleDetailId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int BDStyleId
        {
            get { return bDStyleId; }
            set { bDStyleId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DetailCode
        {
            get { return detailCode; }
            set { detailCode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DetailName
        {
            get { return detailName; }
            set { detailName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum DetailStatus
        {
            get { return detailStatus; }
            set { detailStatus = value; }
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
            get { return this.styleDetailId; }
            set { this.styleDetailId = value; }
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
        /// 数据状态名
        /// </summary>
        public string DetailStatusName
        {
            get { return this.detailStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.BDStyleDetailDAL";
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