
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingApplyDelay.cs
// 文件功能描述：点价申请延期dbo.Pri_PricingApplyDelay实体类。
// 创建人：CodeSmith
// 创建时间： 2014年12月8日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.DoPrice.Model
{
    /// <summary>
    /// 点价申请延期dbo.Pri_PricingApplyDelay实体类。
    /// </summary>
    [Serializable]
    public class PricingApplyDelay : IModel
    {
        #region 字段

        private int delayId;
        private int pricingApplyId;
        private decimal delayAmount;
        private decimal delayFee;
        private DateTime delayQP;
        private Common.StatusEnum detailStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Pri_PricingApplyDelay";
        #endregion

        #region 构造函数

        public PricingApplyDelay()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 明细序号
        /// </summary>
        public int DelayId
        {
            get { return delayId; }
            set { delayId = value; }
        }

        /// <summary>
        /// 点价申请序号
        /// </summary>
        public int PricingApplyId
        {
            get { return pricingApplyId; }
            set { pricingApplyId = value; }
        }

        /// <summary>
        /// 延期重量
        /// </summary>
        public decimal DelayAmount
        {
            get { return delayAmount; }
            set { delayAmount = value; }
        }

        /// <summary>
        /// 延期费
        /// </summary>
        public decimal DelayFee
        {
            get { return delayFee; }
            set { delayFee = value; }
        }

        /// <summary>
        /// 延期Qp
        /// </summary>
        public DateTime DelayQP
        {
            get { return delayQP; }
            set { delayQP = value; }
        }

        /// <summary>
        /// 明细状态
        /// </summary>
        public Common.StatusEnum DetailStatus
        {
            get { return detailStatus; }
            set { detailStatus = value; }
        }

        /// <summary>
        /// 申请创建人
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
        /// 最后修改人
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
            get { return this.delayId; }
            set { this.delayId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return detailStatus; }
            set { detailStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.DoPrice.DAL.PricingApplyDelayDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.DoPrice";
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