
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMoveApplyAttach.cs
// 文件功能描述：移库申请附件dbo.StockMoveApplyAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 移库申请附件dbo.StockMoveApplyAttach实体类。
    /// </summary>
    [Serializable]
    public class StockMoveApplyAttach : IModel
    {
        #region 字段

        private int stockMoveApplyAttachId;
        private int stockMoveApplyId;
        private int attachId;
        private string tableName = "dbo.StockMoveApplyAttach";

        #endregion

        #region 构造函数

        public StockMoveApplyAttach()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 移库申请附件序号
        /// </summary>
        public int StockMoveApplyAttachId
        {
            get { return stockMoveApplyAttachId; }
            set { stockMoveApplyAttachId = value; }
        }

        /// <summary>
        /// 移库申请序号
        /// </summary>
        public int StockMoveApplyId
        {
            get { return stockMoveApplyId; }
            set { stockMoveApplyId = value; }
        }

        /// <summary>
        /// 附件序号
        /// </summary>
        public int AttachId
        {
            get { return attachId; }
            set { attachId = value; }
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
            get { return this.stockMoveApplyAttachId; }
            set { this.stockMoveApplyAttachId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get;
            set;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.StockMoveApplyAttachDAL";
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