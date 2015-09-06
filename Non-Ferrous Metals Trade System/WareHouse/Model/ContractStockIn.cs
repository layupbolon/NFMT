
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractStockIn.cs
// 文件功能描述：入库登记合约关联dbo.St_ContractStockIn_Ref实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月6日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 入库登记合约关联dbo.St_ContractStockIn_Ref实体类。
    /// </summary>
    [Serializable]
    public class ContractStockIn : IModel
    {
        #region 字段

        private int refId;
        private int stockInId;
        private int contractId;
        private int contractSubId;
        private StatusEnum refStatus;
        private string tableName = "dbo.St_ContractStockIn_Ref";
        #endregion

        #region 构造函数

        public ContractStockIn()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 序号
        /// </summary>
        public int RefId
        {
            get { return refId; }
            set { refId = value; }
        }

        /// <summary>
        /// 登记序号
        /// </summary>
        public int StockInId
        {
            get { return stockInId; }
            set { stockInId = value; }
        }

        /// <summary>
        /// 合约序号
        /// </summary>
        public int ContractId
        {
            get { return contractId; }
            set { contractId = value; }
        }

        /// <summary>
        /// 子合约序号
        /// </summary>
        public int ContractSubId
        {
            get { return contractSubId; }
            set { contractSubId = value; }
        }

        /// <summary>
        /// 关联状态
        /// </summary>
        public StatusEnum RefStatus
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

        private string dalName = "NFMT.WareHouse.DAL.ContractStockInDAL";
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