
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockOutAttach.cs
// 文件功能描述：出库申请附件dbo.StockOutAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 出库申请附件dbo.StockOutAttach实体类。
    /// </summary>
    [Serializable]
    public class StockOutAttach : IAttachModel
    {
        #region 字段

        private int stockOutAttachId;
        private int stockOutId;
        private int attachId;
        private string tableName = "dbo.St_StockOutAttach";

        #endregion

        #region 构造函数

        public StockOutAttach()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 出库申请附件序号
        /// </summary>
        public int StockOutAttachId
        {
            get { return stockOutAttachId; }
            set { stockOutAttachId = value; }
        }

        /// <summary>
        /// 出库序号
        /// </summary>
        public int StockOutId
        {
            get { return stockOutId; }
            set { stockOutId = value; }
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
            get { return this.stockOutAttachId; }
            set { this.stockOutAttachId = value; }
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

        private string dalName = "NFMT.WareHouse.DAL.StockOutAttachDAL";
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

        #region 实现接口

        public int BussinessDataId
        {
            get { return stockOutId; }
            set { stockOutId = value; }
        }

        #endregion
    }
}