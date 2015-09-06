
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockOut.cs
// 文件功能描述：出库dbo.St_StockOut实体类。
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 出库dbo.St_StockOut实体类。
    /// </summary>
    [Serializable]
    public class StockOut : IModel
    {
        #region 字段

        private int stockOutId;
        private int stockOutApplyId;
        private int executor;
        private int confirmor;
        private DateTime stockOutTime;
        private decimal grosstAmount;
        private decimal netAmount;
        private int bundles;
        private int unit;
        private int stockOperateType;
        private string memo = String.Empty;
        private StatusEnum stockOutStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.St_StockOut";
        #endregion

        #region 构造函数

        public StockOut()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 出库序号
        /// </summary>
        public int StockOutId
        {
            get { return stockOutId; }
            set { stockOutId = value; }
        }

        /// <summary>
        /// 出库申请序号
        /// </summary>
        public int StockOutApplyId
        {
            get { return stockOutApplyId; }
            set { stockOutApplyId = value; }
        }

        /// <summary>
        /// 出库执行人
        /// </summary>
        public int Executor
        {
            get { return executor; }
            set { executor = value; }
        }

        /// <summary>
        /// 出库确认人
        /// </summary>
        public int Confirmor
        {
            get { return confirmor; }
            set { confirmor = value; }
        }

        /// <summary>
        /// 出库确认时间
        /// </summary>
        public DateTime StockOutTime
        {
            get { return stockOutTime; }
            set { stockOutTime = value; }
        }

        /// <summary>
        /// 出库总毛重
        /// </summary>
        public decimal GrosstAmount
        {
            get { return grosstAmount; }
            set { grosstAmount = value; }
        }

        /// <summary>
        /// 出库总净重
        /// </summary>
        public decimal NetAmount
        {
            get { return netAmount; }
            set { netAmount = value; }
        }

        /// <summary>
        /// 出库总捆数
        /// </summary>
        public int Bundles
        {
            get { return bundles; }
            set { bundles = value; }
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
        /// 出入库类型
        /// </summary>
        public int StockOperateType
        {
            get { return stockOperateType; }
            set { stockOperateType = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        /// <summary>
        /// 出库状态
        /// </summary>
        public StatusEnum StockOutStatus
        {
            get { return stockOutStatus; }
            set { stockOutStatus = value; }
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
            get { return this.stockOutId; }
            set { this.stockOutId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return stockOutStatus; }
            set { stockOutStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.StockOutDAL";
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