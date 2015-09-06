
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Asset.cs
// 文件功能描述：品种表dbo.Asset实体类。
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
    /// <summary>
    /// 品种表dbo.Asset实体类。
    /// </summary>
    [Serializable]
    public class Asset : IModel
    {
        #region 字段

        private int assetId;
        private string assetName = String.Empty;
        private int mUId;
        private decimal misTake;
        private int amountPerHand;
        private Common.StatusEnum assetStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Asset";
        #endregion

        #region 构造函数

        public Asset()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int AssetId
        {
            get { return assetId; }
            set { assetId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AssetName
        {
            get { return assetName; }
            set { assetName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MUId
        {
            get { return mUId; }
            set { mUId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal MisTake
        {
            get { return misTake; }
            set { misTake = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AmountPerHand
        {
            get { return amountPerHand; }
            set { amountPerHand = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum AssetStatus
        {
            get { return assetStatus; }
            set { assetStatus = value; }
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
            get { return this.assetId; }
            set { this.assetId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return assetStatus; }
            set { assetStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.AssetDAL";
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