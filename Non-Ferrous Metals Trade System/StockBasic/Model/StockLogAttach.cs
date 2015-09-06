using NFMT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.StockBasic.Model
{
    /// <summary>
    /// 流水附件dbo.StockLogAttach实体类。
    /// </summary>
    [Serializable]
    public class StockLogAttach : IAttachModel
    {
        #region 字段

        private int stockLogAttachId;
        private int stockLogId;
        private int attachId;
        private string tableName = "dbo.StockLogAttach";

        #endregion

        #region 构造函数

        public StockLogAttach()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 流水附件序号
        /// </summary>
        public int StockLogAttachId
        {
            get { return stockLogAttachId; }
            set { stockLogAttachId = value; }
        }

        /// <summary>
        /// 库存流水序号
        /// </summary>
        public int StockLogId
        {
            get { return stockLogId; }
            set { stockLogId = value; }
        }

        /// <summary>
        /// 附件主表序号
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
            get { return this.stockLogAttachId; }
            set { this.stockLogAttachId = value; }
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

        private string dalName = "NFMT.StockBasic.DAL.StockLogAttachDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.StockBasic";
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
            get { return stockLogId; }
            set { stockLogId = value; }
        }

        #endregion
    }
}
