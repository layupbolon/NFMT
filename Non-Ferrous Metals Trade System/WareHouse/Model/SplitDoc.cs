
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SplitDoc.cs
// 文件功能描述：拆单dbo.St_SplitDoc实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月18日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 拆单dbo.St_SplitDoc实体类。
    /// </summary>
    [Serializable]
    public class SplitDoc : IModel
    {
        #region 字段

        private int splitDocId;
        private int spliter;
        private DateTime splitDocTime;
        private Common.StatusEnum splitDocStatus;
        private int oldRefNoId;
        private string oldRefNo = String.Empty;
        private int oldStockId;
        private int stockLogId;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.St_SplitDoc";
        #endregion

        #region 构造函数

        public SplitDoc()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 拆单序号
        /// </summary>
        public int SplitDocId
        {
            get { return splitDocId; }
            set { splitDocId = value; }
        }

        /// <summary>
        /// 拆单人
        /// </summary>
        public int Spliter
        {
            get { return spliter; }
            set { spliter = value; }
        }

        /// <summary>
        /// 拆单时间
        /// </summary>
        public DateTime SplitDocTime
        {
            get { return splitDocTime; }
            set { splitDocTime = value; }
        }

        /// <summary>
        /// 拆单状态
        /// </summary>
        public Common.StatusEnum SplitDocStatus
        {
            get { return splitDocStatus; }
            set { splitDocStatus = value; }
        }

        /// <summary>
        /// 原业务单号
        /// </summary>
        public int OldRefNoId
        {
            get { return oldRefNoId; }
            set { oldRefNoId = value; }
        }

        /// <summary>
        /// 原业务单号
        /// </summary>
        public string OldRefNo
        {
            get { return oldRefNo; }
            set { oldRefNo = value; }
        }

        /// <summary>
        /// 原库存号
        /// </summary>
        public int OldStockId
        {
            get { return oldStockId; }
            set { oldStockId = value; }
        }

        /// <summary>
        /// 拆单流水序号
        /// </summary>
        public int StockLogId
        {
            get { return stockLogId; }
            set { stockLogId = value; }
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
            get { return this.splitDocId; }
            set { this.splitDocId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return splitDocStatus; }
            set { splitDocStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.SplitDocDAL";
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