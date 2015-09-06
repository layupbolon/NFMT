
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Sms.cs
// 文件功能描述：消息dbo.Sm_Sms实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Sms.Model
{
    /// <summary>
    /// 消息dbo.Sm_Sms实体类。
    /// </summary>
    [Serializable]
    public class Sms : IModel
    {
        #region 字段

        private int smsId;
        private int smsTypeId;
        private string smsHead = String.Empty;
        private string smsBody = String.Empty;
        private DateTime smsRelTime;
        private int smsStatus;
        private int smsLevel;
        private int creatorId;
        private DateTime createTime;
        private int sourceId;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Sm_Sms";
        #endregion

        #region 构造函数

        public Sms()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 消息序号
        /// </summary>
        public int SmsId
        {
            get { return smsId; }
            set { smsId = value; }
        }

        /// <summary>
        /// 消息类别
        /// </summary>
        public int SmsTypeId
        {
            get { return smsTypeId; }
            set { smsTypeId = value; }
        }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string SmsHead
        {
            get { return smsHead; }
            set { smsHead = value; }
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string SmsBody
        {
            get { return smsBody; }
            set { smsBody = value; }
        }

        /// <summary>
        /// 消息发布时间
        /// </summary>
        public DateTime SmsRelTime
        {
            get { return smsRelTime; }
            set { smsRelTime = value; }
        }

        /// <summary>
        /// 消息状态（0=无效消息，1=待处理消息，2=已处理消息）
        /// </summary>
        public int SmsStatus
        {
            get { return smsStatus; }
            set { smsStatus = value; }
        }

        /// <summary>
        /// 消息优先级
        /// </summary>
        public int SmsLevel
        {
            get { return smsLevel; }
            set { smsLevel = value; }
        }

        /// <summary>
        /// 发起人序号
        /// </summary>
        public int CreatorId
        {
            get { return creatorId; }
            set { creatorId = value; }
        }

        /// <summary>
        /// 发起时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 数据序号
        /// </summary>
        public int SourceId
        {
            get { return sourceId; }
            set { sourceId = value; }
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
            get { return this.smsId; }
            set { this.smsId = value; }
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

        private string dalName = "NFMT.Sms.DAL.SmsDAL";
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