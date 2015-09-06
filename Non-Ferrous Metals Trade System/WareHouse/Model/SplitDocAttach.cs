
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SplitDocAttach.cs
// 文件功能描述：拆单附件dbo.St_SplitDocAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年10月27日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 拆单附件dbo.St_SplitDocAttach实体类。
    /// </summary>
    [Serializable]
    public class SplitDocAttach : IAttachModel
    {
        #region 字段

        private int splitDocAttachId;
        private int splitDocId;
        private int splitDocDetailId;
        private int attachId;
        private string tableName = "dbo.St_SplitDocAttach";
        #endregion

        #region 构造函数

        public SplitDocAttach()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 拆单附件序号
        /// </summary>
        public int SplitDocAttachId
        {
            get { return splitDocAttachId; }
            set { splitDocAttachId = value; }
        }

        /// <summary>
        /// 拆单序号
        /// </summary>
        public int SplitDocId
        {
            get { return splitDocId; }
            set { splitDocId = value; }
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
        /// 明细状态
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
            get { return this.splitDocAttachId; }
            set { this.splitDocAttachId = value; }
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

        private string dalName = "NFMT.WareHouse.DAL.SplitDocAttachDAL";
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
            get { return splitDocId; }
            set { splitDocId = value; }
        }

        #endregion
    }
}