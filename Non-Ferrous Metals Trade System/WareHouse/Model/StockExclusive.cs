
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockExclusive.cs
// 文件功能描述：库存申请库存排他表dbo.St_StockExclusive实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月15日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 库存申请库存排他表dbo.St_StockExclusive实体类。
    /// </summary>
    [Serializable]
    public class StockExclusive : IModel
    {
        #region 字段

        private int exclusiveId;
        private int applyId;
        private int stockApplyId;
        private int detailApplyId;
        private int stockId;
        private decimal exclusiveAmount;
        private StatusEnum exclusiveStatus;
        private string tableName = "dbo.St_StockExclusive";
        #endregion

        #region 构造函数

        public StockExclusive()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 排它序号
        /// </summary>
        public int ExclusiveId
        {
            get { return exclusiveId; }
            set { exclusiveId = value; }
        }

        /// <summary>
        /// 申请序号
        /// </summary>
        public int ApplyId
        {
            get { return applyId; }
            set { applyId = value; }
        }

        /// <summary>
        /// 库存申请序号
        /// </summary>
        public int StockApplyId
        {
            get { return stockApplyId; }
            set { stockApplyId = value; }
        }

        /// <summary>
        /// 库存申请明细序号
        /// </summary>
        public int DetailApplyId
        {
            get { return detailApplyId; }
            set { detailApplyId = value; }
        }

        /// <summary>
        /// 库存明细
        /// </summary>
        public int StockId
        {
            get { return stockId; }
            set { stockId = value; }
        }

        /// <summary>
        /// 排他重量
        /// </summary>
        public decimal ExclusiveAmount
        {
            get { return exclusiveAmount; }
            set { exclusiveAmount = value; }
        }

        /// <summary>
        /// 排他状态
        /// </summary>
        public StatusEnum ExclusiveStatus
        {
            get { return exclusiveStatus; }
            set { exclusiveStatus = value; }
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
            get { return this.exclusiveId; }
            set { this.exclusiveId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return exclusiveStatus; }
            set { exclusiveStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.StockExclusiveDAL";
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