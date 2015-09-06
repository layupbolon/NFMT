
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoDetail.cs
// 文件功能描述：回购明细dbo.St_RepoDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月17日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 回购明细dbo.St_RepoDetail实体类。
    /// </summary>
    [Serializable]
    public class RepoDetail : IModel
    {
        #region 字段

        private int detailId;
        private int repoId;
        private Common.StatusEnum repoDetailStatus;
        private int stockId;
        private int repoApplyDetailId;
        private decimal repoWeight;
        private int unit;
        private decimal repoPrice;
        private int currency;
        private int stockLogId;
        private string tableName = "dbo.St_RepoDetail";
        #endregion

        #region 构造函数

        public RepoDetail()
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
        /// 回购序号
        /// </summary>
        public int RepoId
        {
            get { return repoId; }
            set { repoId = value; }
        }

        /// <summary>
        /// 回购明细状态
        /// </summary>
        public Common.StatusEnum RepoDetailStatus
        {
            get { return repoDetailStatus; }
            set { repoDetailStatus = value; }
        }

        /// <summary>
        /// 库存序号
        /// </summary>
        public int StockId
        {
            get { return stockId; }
            set { stockId = value; }
        }

        /// <summary>
        /// 回购申请明细序号
        /// </summary>
        public int RepoApplyDetailId
        {
            get { return repoApplyDetailId; }
            set { repoApplyDetailId = value; }
        }

        /// <summary>
        /// 回购重量
        /// </summary>
        public decimal RepoWeight
        {
            get { return repoWeight; }
            set { repoWeight = value; }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public int Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        /// <summary>
        /// 回购价格
        /// </summary>
        public decimal RepoPrice
        {
            get { return repoPrice; }
            set { repoPrice = value; }
        }

        /// <summary>
        /// 币种
        /// </summary>
        public int Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        /// <summary>
        /// 回购流水序号
        /// </summary>
        public int StockLogId
        {
            get { return stockLogId; }
            set { stockLogId = value; }
        }

        public int CreatorId { get; set; }
        public DateTime CreateTime { get; set; }
        public int LastModifyId { get; set; }
        public DateTime LastModifyTime { get; set; }

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
            get { return repoDetailStatus; }
            set { repoDetailStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.RepoDetailDAL";
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