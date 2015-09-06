
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Repo.cs
// 文件功能描述：回购dbo.St_Repo实体类。
// 创建人：CodeSmith
// 创建时间： 2014年8月27日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 回购dbo.St_Repo实体类。
    /// </summary>
    [Serializable]
    public class Repo : IModel
    {
        #region 字段

        private int repoId;
        private int repoApplyId;
        private int repoer;
        private DateTime repoerTime;
        private Common.StatusEnum repoStatus;
        private string memo = String.Empty;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.St_Repo";
        #endregion

        #region 构造函数

        public Repo()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 回购序号
        /// </summary>
        public int RepoId
        {
            get { return repoId; }
            set { repoId = value; }
        }

        /// <summary>
        /// 回购申请序号
        /// </summary>
        public int RepoApplyId
        {
            get { return repoApplyId; }
            set { repoApplyId = value; }
        }

        /// <summary>
        /// 回购确认人
        /// </summary>
        public int Repoer
        {
            get { return repoer; }
            set { repoer = value; }
        }

        /// <summary>
        /// 回购确认时间
        /// </summary>
        public DateTime RepoerTime
        {
            get { return repoerTime; }
            set { repoerTime = value; }
        }

        /// <summary>
        /// 回购状态
        /// </summary>
        public Common.StatusEnum RepoStatus
        {
            get { return repoStatus; }
            set { repoStatus = value; }
        }

        /// <summary>
        /// 附言
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
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
            get { return this.repoId; }
            set { this.repoId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return repoStatus; }
            set { repoStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.RepoDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.WareHouse";
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