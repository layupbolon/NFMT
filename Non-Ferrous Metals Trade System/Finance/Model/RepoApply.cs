
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoApply.cs
// 文件功能描述：融资头寸赎回申请单dbo.Fin_RepoApply实体类。
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Finance.Model
{
    /// <summary>
    /// 融资头寸赎回申请单dbo.Fin_RepoApply实体类。
    /// </summary>
    [Serializable]
    public class RepoApply : IModel
    {
        #region 字段

        private int repoApplyId;
        private string repoApplyIdNo = String.Empty;
        private int pledgeApplyId;
        private decimal sumNetAmount;
        private int sumHands;
        private Common.StatusEnum repoApplyStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Fin_RepoApply";
        #endregion

        #region 构造函数

        public RepoApply()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public int RepoApplyId
        {
            get { return repoApplyId; }
            set { repoApplyId = value; }
        }

        /// <summary>
        /// 赎回申请单号
        /// </summary>
        public string RepoApplyIdNo
        {
            get { return repoApplyIdNo; }
            set { repoApplyIdNo = value; }
        }

        /// <summary>
        /// 质押申请序号
        /// </summary>
        public int PledgeApplyId
        {
            get { return pledgeApplyId; }
            set { pledgeApplyId = value; }
        }

        /// <summary>
        /// 净重合计
        /// </summary>
        public decimal SumNetAmount
        {
            get { return sumNetAmount; }
            set { sumNetAmount = value; }
        }

        /// <summary>
        /// 手数合计
        /// </summary>
        public int SumHands
        {
            get { return sumHands; }
            set { sumHands = value; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public Common.StatusEnum RepoApplyStatus
        {
            get { return repoApplyStatus; }
            set { repoApplyStatus = value; }
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
            get { return this.repoApplyId; }
            set { this.repoApplyId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return repoApplyStatus; }
            set { repoApplyStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Finance.DAL.RepoApplyDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Finance";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "Financing";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}