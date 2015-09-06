
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockName.cs
// 文件功能描述：业务单号表dbo.St_StockName实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月30日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 业务单号表dbo.St_StockName实体类。
    /// </summary>
    [Serializable]
    public class StockName : IModel
    {
        #region 字段

        private int stockNameId;
        private string refNo = String.Empty;
        private string tableName = "dbo.St_StockName";
        #endregion

        #region 构造函数

        public StockName()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 库存名称序号
        /// </summary>
        public int StockNameId
        {
            get { return stockNameId; }
            set { stockNameId = value; }
        }

        /// <summary>
        /// 业务单号
        /// </summary>
        public string RefNo
        {
            get { return refNo; }
            set { refNo = value; }
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
            get { return this.stockNameId; }
            set { this.stockNameId = value; }
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

        private string dalName = "NFMT.WareHouse.DAL.StockNameDAL";
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