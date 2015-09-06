
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeAttach.cs
// 文件功能描述：质押附件dbo.St_PledgeAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月27日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 质押附件dbo.St_PledgeAttach实体类。
    /// </summary>
    [Serializable]
    public class PledgeAttach : IAttachModel
    {
        #region 字段

        private int pledgeAttachId;
        private int pledgeId;
        private int attachId;
        private string tableName = "dbo.St_PledgeAttach";
        #endregion

        #region 构造函数

        public PledgeAttach()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 质押附件序号
        /// </summary>
        public int PledgeAttachId
        {
            get { return pledgeAttachId; }
            set { pledgeAttachId = value; }
        }

        /// <summary>
        /// 质押序号
        /// </summary>
        public int PledgeId
        {
            get { return pledgeId; }
            set { pledgeId = value; }
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
            get { return this.pledgeAttachId; }
            set { this.pledgeAttachId = value; }
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

        private string dalName = "NFMT.WareHouse.DAL.PledgeAttachDAL";
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

        public int BussinessDataId
        {
            get { return pledgeId; }
            set { pledgeId = value; }
        }
    }
}