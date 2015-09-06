
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CustomsApplyAttach.cs
// 文件功能描述：报关申请附件dbo.St_CustomsApplyAttach实体类。
// 创建人：CodeSmith
// 创建时间： 2014年10月29日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 报关申请附件dbo.St_CustomsApplyAttach实体类。
    /// </summary>
    [Serializable]
    public class CustomsApplyAttach : IAttachModel
    {
        #region 字段

        private int customsApplyAttachId;
        private int customsApplyId;
        private int attachId;
        private string tableName = "dbo.St_CustomsApplyAttach";
        #endregion

        #region 构造函数

        public CustomsApplyAttach()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 报关申请附件序号
        /// </summary>
        public int CustomsApplyAttachId
        {
            get { return customsApplyAttachId; }
            set { customsApplyAttachId = value; }
        }

        /// <summary>
        /// 报关申请序号
        /// </summary>
        public int CustomsApplyId
        {
            get { return customsApplyId; }
            set { customsApplyId = value; }
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
            get { return this.customsApplyAttachId; }
            set { this.customsApplyAttachId = value; }
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

        private string dalName = "NFMT.WareHouse.DAL.CustomsApplyAttachDAL";
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
            get { return customsApplyId; }
            set { customsApplyId = value; }
        }
        #endregion
    }
}