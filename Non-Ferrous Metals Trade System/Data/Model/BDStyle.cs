/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：BDStyle.cs
// 文件功能描述：dbo.BDStyle实体类。
// 创建人：CodeSmith
// 创建时间： 2014年4月22日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
    /// <summary>
    /// dbo.BDStyle实体类。
    /// </summary>
    [Serializable]
    public class BDStyle : IModel
    {
        #region 字段

        private int bDStyleId;
        private string bDStyleCode = String.Empty;
        private string bDStyleName = String.Empty;
        private Common.StatusEnum bDStyleStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = string.Empty;
        #endregion

        #region 构造函数

        public BDStyle()
        {
            this.tableName = "BDStyle";
        }

        #endregion

        #region 属性

        /// <summary>
        /// 类型表序号
        /// </summary>
        public int Id
        {
            get { return this.bDStyleId; }
            set { this.bDStyleId = value; }
        }

        /// <summary>
        /// 类型表状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return this.bDStyleStatus; }
            set { this.bDStyleStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get
            {
                return this.tableName;
            }
            set
            {
                this.tableName = value;
            }
        }

        /// <summary>
        /// 类型表序号
        /// </summary>
        public int BDStyleId
        {
            get { return bDStyleId; }
            set { bDStyleId = value; }
        }

        /// <summary>
        /// 类型编码
        /// </summary>
        public string BDStyleCode
        {
            get { return bDStyleCode; }
            set { bDStyleCode = value; }
        }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string BDStyleName
        {
            get { return bDStyleName; }
            set { bDStyleName = value; }
        }

        /// <summary>
        /// 类型状态
        /// </summary>
        public Common.StatusEnum BDStyleStatus
        {
            get { return bDStyleStatus; }
            set { bDStyleStatus = value; }
        }

        public string BDStyleStatusName
        {
            get { return this.bDStyleStatus.ToString(); }
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

        private string dalName = "NFMT.Data.DAL.BDStyleDAL";
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