
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsDetail.cs
// 文件功能描述：消息明细表dbo.Sm_SmsDetail实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月11日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Sms.Model
{
    /// <summary>
    /// 消息明细表dbo.Sm_SmsDetail实体类。
    /// </summary>
    [Serializable]
    public class SmsDetail : IModel
    {
        #region 字段

        private int detailId;
        private int smsId;
        private int empId;
        private DateTime readTime;
        private int detailStatus;
        private string tableName = "dbo.Sm_SmsDetail";
        #endregion

        #region 构造函数

        public SmsDetail()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 已读序号
        /// </summary>
        public int DetailId
        {
            get { return detailId; }
            set { detailId = value; }
        }

        /// <summary>
        /// 消息序号
        /// </summary>
        public int SmsId
        {
            get { return smsId; }
            set { smsId = value; }
        }

        /// <summary>
        /// 员工人
        /// </summary>
        public int EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        /// <summary>
        /// 读取时间
        /// </summary>
        public DateTime ReadTime
        {
            get { return readTime; }
            set { readTime = value; }
        }

        /// <summary>
        /// 明细状态
        /// </summary>
        public int DetailStatus
        {
            get { return detailStatus; }
            set { detailStatus = value; }
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
            get { return this.detailId; }
            set { this.detailId = value; }
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

        private string dalName = "NFMT.Sms.DAL.SmsDetailDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Sms";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT_Sms";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}