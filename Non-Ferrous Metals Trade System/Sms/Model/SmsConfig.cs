
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsConfig.cs
// 文件功能描述：消息配置dbo.Sm_SmsConfig实体类。
// 创建人：CodeSmith
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Sms.Model
{
    /// <summary>
    /// 消息配置dbo.Sm_SmsConfig实体类。
    /// </summary>
    [Serializable]
    public class SmsConfig : IModel
    {
        #region 字段

        private int smsConfigId;
        private string empId = String.Empty;
        private Common.StatusEnum configStatus = 0;
        private string tableName = "dbo.Sm_SmsConfig";

        #endregion

        #region 构造函数

        public SmsConfig()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 消息配置序号
        /// </summary>
        public int SmsConfigId
        {
            get { return smsConfigId; }
            set { smsConfigId = value; }
        }

        /// <summary>
        /// 配置人序号
        /// </summary>
        public string EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        /// <summary>
        /// 配置状态
        /// </summary>
        public Common.StatusEnum ConfigStatus
        {
            get { return configStatus; }
            set { configStatus = value; }
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
            get { return this.smsConfigId; }
            set { this.smsConfigId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return configStatus; }
            set { configStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Sms.DAL.SmsConfigDAL";
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