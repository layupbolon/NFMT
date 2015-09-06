
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorpDetailAttach.cs
// 文件功能描述：客户附件表dbo.CorpDetailAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2015年1月22日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
    /// <summary>
    /// 客户附件表dbo.CorpDetailAttach实体类。
    /// </summary>
    [Serializable]
    public class CorpDetailAttach : IAttachModel
    {
        #region 字段

        private int corpDetailAttachId;
        private int detailId;
        private int attachId;
        private int attachType;
        private Common.StatusEnum corpDetailAttachStatus;
        private string tableName = "dbo.CorpDetailAttach";
        #endregion

        #region 构造函数

        public CorpDetailAttach()
        {
        }

        public CorpDetailAttach(NFMT.Operate.AttachType _attachType)
        {
            this.attachType = (int)_attachType;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 关联表序号
        /// </summary>
        public int CorpDetailAttachId
        {
            get { return corpDetailAttachId; }
            set { corpDetailAttachId = value; }
        }

        /// <summary>
        /// 客户明细序号
        /// </summary>
        public int DetailId
        {
            get { return detailId; }
            set { detailId = value; }
        }

        /// <summary>
        /// 附件序号
        /// </summary>
        public int AttachId
        {
            get { return attachId; }
            set { attachId = value; }
        }

        /// <summary>
        /// 附件类型
        /// </summary>
        public int AttachType
        {
            get { return attachType; }
            set { attachType = value; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public Common.StatusEnum CorpDetailAttachStatus
        {
            get { return corpDetailAttachStatus; }
            set { corpDetailAttachStatus = value; }
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
            get { return this.corpDetailAttachId; }
            set { this.corpDetailAttachId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return corpDetailAttachStatus; }
            set { corpDetailAttachStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.User.DAL.CorpDetailAttachDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.User";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT_User";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion

        #region 实现接口

        public int BussinessDataId
        {
            get { return detailId; }
            set { detailId = value; }
        }
        #endregion
    }
}