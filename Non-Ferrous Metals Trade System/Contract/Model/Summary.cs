
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Summary.cs
// 文件功能描述：制单dbo.Con_Summary实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Contract.Model
{
    /// <summary>
    /// 制单dbo.Con_Summary实体类。
    /// </summary>
    [Serializable]
    public class Summary : IModel
    {
        #region 字段

        private int summaryId;
        private int summaryApplyId;
        private DateTime summaryDate;
        private int summaryEmpId;
        private string meno = String.Empty;
        private int summaryStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Con_Summary";

        #endregion

        #region 构造函数

        public Summary()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 制单序号
        /// </summary>
        public int SummaryId
        {
            get { return summaryId; }
            set { summaryId = value; }
        }

        /// <summary>
        /// 制单申请序号
        /// </summary>
        public int SummaryApplyId
        {
            get { return summaryApplyId; }
            set { summaryApplyId = value; }
        }

        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime SummaryDate
        {
            get { return summaryDate; }
            set { summaryDate = value; }
        }

        /// <summary>
        /// 制单人
        /// </summary>
        public int SummaryEmpId
        {
            get { return summaryEmpId; }
            set { summaryEmpId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Meno
        {
            get { return meno; }
            set { meno = value; }
        }

        /// <summary>
        /// 制单状态
        /// </summary>
        public int SummaryStatus
        {
            get { return summaryStatus; }
            set { summaryStatus = value; }
        }

        /// <summary>
        /// 创建人
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
            get { return this.summaryId; }
            set { this.summaryId = value; }
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

        private string dalName = "NFMT.Contract.DAL.SummaryDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Contract";
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