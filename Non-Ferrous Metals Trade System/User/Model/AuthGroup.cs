
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AuthGroup.cs
// 文件功能描述：权限组dbo.AuthGroup实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月18日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
    /// <summary>
    /// 权限组dbo.AuthGroup实体类。
    /// </summary>
    [Serializable]
    public class AuthGroup : IModel
    {
        #region 字段

        private int authGroupId;
        private string authGroupName = String.Empty;
        private int assetId;
        private int tradeDirection;
        private int tradeBorder;
        private int contractInOut;
        private int contractLimit;
        private int corpId;
        private Common.StatusEnum authGroupStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.AuthGroup";
        #endregion

        #region 构造函数

        public AuthGroup()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 权限组序号
        /// </summary>
        public int AuthGroupId
        {
            get { return authGroupId; }
            set { authGroupId = value; }
        }

        /// <summary>
        /// 权限组名称
        /// </summary>
        public string AuthGroupName
        {
            get { return authGroupName; }
            set { authGroupName = value; }
        }

        /// <summary>
        /// 品种
        /// </summary>
        public int AssetId
        {
            get { return assetId; }
            set { assetId = value; }
        }

        /// <summary>
        /// 贸易方向（进口，出口）
        /// </summary>
        public int TradeDirection
        {
            get { return tradeDirection; }
            set { tradeDirection = value; }
        }

        /// <summary>
        /// 贸易方向（外贸，内贸）
        /// </summary>
        public int TradeBorder
        {
            get { return tradeBorder; }
            set { tradeBorder = value; }
        }

        /// <summary>
        /// 内外部合约
        /// </summary>
        public int ContractInOut
        {
            get { return contractInOut; }
            set { contractInOut = value; }
        }

        /// <summary>
        /// 合约长零单
        /// </summary>
        public int ContractLimit
        {
            get { return contractLimit; }
            set { contractLimit = value; }
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
        /// 权限组状态
        /// </summary>
        public Common.StatusEnum AuthGroupStatus
        {
            get { return authGroupStatus; }
            set { authGroupStatus = value; }
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
            get { return this.authGroupId; }
            set { this.authGroupId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return authGroupStatus; }
            set { authGroupStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.User.DAL.AuthGroupDAL";
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