
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SplitDocStock.cs
// 文件功能描述：拆单库存关联表dbo.St_SplitDocStock_Ref实体类。
// 创建人：CodeSmith
// 创建时间： 2014年10月28日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 拆单库存关联表dbo.St_SplitDocStock_Ref实体类。
    /// </summary>
    [Serializable]
    public class SplitDocStock : IModel
    {
        #region 字段

        private int refId;
        private int splitDocDetailId;
        private int newRefNoId;
        private int newStockId;
        private int oldRefNoId;
        private int oldStockId;
        private Common.StatusEnum refStatus;
        private string tableName = "dbo.St_SplitDocStock_Ref";
        #endregion

        #region 构造函数

        public SplitDocStock()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 关联序号
        /// </summary>
        public int RefId
        {
            get { return refId; }
            set { refId = value; }
        }

        /// <summary>
        /// 拆单明细序号
        /// </summary>
        public int SplitDocDetailId
        {
            get { return splitDocDetailId; }
            set { splitDocDetailId = value; }
        }

        /// <summary>
        /// 新业务单序号
        /// </summary>
        public int NewRefNoId
        {
            get { return newRefNoId; }
            set { newRefNoId = value; }
        }

        /// <summary>
        /// 库存序号
        /// </summary>
        public int NewStockId
        {
            get { return newStockId; }
            set { newStockId = value; }
        }

        /// <summary>
        /// 旧业务单序号
        /// </summary>
        public int OldRefNoId
        {
            get { return oldRefNoId; }
            set { oldRefNoId = value; }
        }

        /// <summary>
        /// 旧库存序号
        /// </summary>
        public int OldStockId
        {
            get { return oldStockId; }
            set { oldStockId = value; }
        }

        /// <summary>
        /// 关联状态
        /// </summary>
        public Common.StatusEnum RefStatus
        {
            get { return refStatus; }
            set { refStatus = value; }
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
            get { return this.refId; }
            set { this.refId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return refStatus; }
            set { refStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.SplitDocStockDAL";
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