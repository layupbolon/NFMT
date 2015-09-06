
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FinBusInvAllot.cs
// 文件功能描述：业务发票财务发票分配dbo.Inv_FinBusInvAllot实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月24日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Invoice.Model
{
    /// <summary>
    /// 业务发票财务发票分配dbo.Inv_FinBusInvAllot实体类。
    /// </summary>
    [Serializable]
    public class FinBusInvAllot : IModel
    {
        #region 字段

        private int allotId;
        private decimal allotBala;
        private int currencyId;
        private int alloter;
        private DateTime allotDate;
        private Common.StatusEnum allotStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Inv_FinBusInvAllot";
        #endregion

        #region 构造函数

        public FinBusInvAllot()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 分配序号
        /// </summary>
        public int AllotId
        {
            get { return allotId; }
            set { allotId = value; }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal AllotBala
        {
            get { return allotBala; }
            set { allotBala = value; }
        }

        /// <summary>
        /// 币种
        /// </summary>
        public int CurrencyId
        {
            get { return currencyId; }
            set { currencyId = value; }
        }

        /// <summary>
        /// 分配人
        /// </summary>
        public int Alloter
        {
            get { return alloter; }
            set { alloter = value; }
        }

        /// <summary>
        /// 分配时间
        /// </summary>
        public DateTime AllotDate
        {
            get { return allotDate; }
            set { allotDate = value; }
        }

        /// <summary>
        /// 分配状态
        /// </summary>
        public Common.StatusEnum AllotStatus
        {
            get { return allotStatus; }
            set { allotStatus = value; }
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
            get { return this.allotId; }
            set { this.allotId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return allotStatus; }
            set { allotStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Invoice.DAL.FinBusInvAllotDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Invoice";
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