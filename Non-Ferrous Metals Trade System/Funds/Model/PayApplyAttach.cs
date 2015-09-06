
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PayApplyAttach.cs
// 文件功能描述：付款申请附件dbo.Fun_PayApplyAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2015年3月13日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Funds.Model
{
    /// <summary>
    /// 付款申请附件dbo.Fun_PayApplyAttach实体类。
    /// </summary>
    [Serializable]
    public class PayApplyAttach : IAttachModel
    {
        #region 字段

        private int payApplyAttachId;
        private int attachId;
        private int payApplyId;
        private int attachType;
        private string tableName = "dbo.Fun_PayApplyAttach";
        #endregion

        #region 构造函数

        public PayApplyAttach()
        {
        }

        public PayApplyAttach(NFMT.Operate.AttachType _attachType)
        {
            this.attachType = (int)_attachType;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 付款申请附件序号
        /// </summary>
        public int PayApplyAttachId
        {
            get { return payApplyAttachId; }
            set { payApplyAttachId = value; }
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
        /// 付款申请序号
        /// </summary>
        public int PayApplyId
        {
            get { return payApplyId; }
            set { payApplyId = value; }
        }

        /// <summary>
        /// 附件类型
        /// </summary>
        public int AttachType
        {
            get { return attachType; }
            set { attachType = value; }
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
            get { return this.payApplyAttachId; }
            set { this.payApplyAttachId = value; }
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

        private string dalName = "NFMT.Funds.DAL.PayApplyAttachDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Funds";
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
            get { return payApplyId; }
            set { payApplyId = value; }
        }
    }
}