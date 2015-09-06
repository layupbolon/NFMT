
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Pledge.cs
// 文件功能描述：质押dbo.St_Pledge实体类。
// 创建人：CodeSmith
// 创建时间： 2014年11月27日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.WareHouse.Model
{
    /// <summary>
    /// 质押dbo.St_Pledge实体类。
    /// </summary>
    [Serializable]
    public class Pledge : IModel
    {
        #region 字段

        private int pledgeId;
        private int pledgeApplyId;
        private int pledger;
        private DateTime pledgeTime;
        private int pledgeBank;
        private string memo = String.Empty;
        private Common.StatusEnum pledgeStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.St_Pledge";
        #endregion

        #region 构造函数

        public Pledge()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 质押序号
        /// </summary>
        public int PledgeId
        {
            get { return pledgeId; }
            set { pledgeId = value; }
        }

        /// <summary>
        /// 质押申请序号
        /// </summary>
        public int PledgeApplyId
        {
            get { return pledgeApplyId; }
            set { pledgeApplyId = value; }
        }

        /// <summary>
        /// 质押执行人
        /// </summary>
        public int Pledger
        {
            get { return pledger; }
            set { pledger = value; }
        }

        /// <summary>
        /// 质押确认时间
        /// </summary>
        public DateTime PledgeTime
        {
            get { return pledgeTime; }
            set { pledgeTime = value; }
        }

        /// <summary>
        /// 质押银行
        /// </summary>
        public int PledgeBank
        {
            get { return pledgeBank; }
            set { pledgeBank = value; }
        }

        /// <summary>
        /// 附言
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        /// <summary>
        /// 质押状态
        /// </summary>
        public Common.StatusEnum PledgeStatus
        {
            get { return pledgeStatus; }
            set { pledgeStatus = value; }
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
            get { return this.pledgeId; }
            set { this.pledgeId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return pledgeStatus; }
            set { pledgeStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.WareHouse.DAL.PledgeDAL";
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