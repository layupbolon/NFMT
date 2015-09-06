
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsType.cs
// 文件功能描述：消息类型dbo.Sm_SmsType实体类。
// 创建人：CodeSmith
// 创建时间： 2014年9月16日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Sms.Model
{
    /// <summary>
    /// 消息类型dbo.Sm_SmsType实体类。
    /// </summary>
    [Serializable]
    public class SmsType : IModel
    {
        #region 字段

        private int smsTypeId;
        private string typeName = String.Empty;
        private string listUrl = String.Empty;
        private string viewUrl = String.Empty;
        private Common.StatusEnum smsTypeStatus;
        private string sourceBaseName = String.Empty;
        private string sourceTableName = String.Empty;
        private string tableName = "dbo.Sm_SmsType";
        #endregion

        #region 构造函数

        public SmsType()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 消息类型序号
        /// </summary>
        public int SmsTypeId
        {
            get { return smsTypeId; }
            set { smsTypeId = value; }
        }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string ListUrl
        {
            get { return listUrl; }
            set { listUrl = value; }
        }

        /// <summary>
        /// 类名
        /// </summary>
        public string ViewUrl
        {
            get { return viewUrl; }
            set { viewUrl = value; }
        }

        /// <summary>
        /// 类型状态
        /// </summary>
        public Common.StatusEnum SmsTypeStatus
        {
            get { return smsTypeStatus; }
            set { smsTypeStatus = value; }
        }

        /// <summary>
        /// 数据库名
        /// </summary>
        public string SourceBaseName
        {
            get { return sourceBaseName; }
            set { sourceBaseName = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string SourceTableName
        {
            get { return sourceTableName; }
            set { sourceTableName = value; }
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
            get { return this.smsTypeId; }
            set { this.smsTypeId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return smsTypeStatus; }
            set { smsTypeStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Sms.DAL.SmsTypeDAL";
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