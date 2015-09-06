
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SmsParameter.cs
// 文件功能描述：消息类型构造参数dbo.Sm_SmsParameter实体类。
// 创建人：CodeSmith
// 创建时间： 2014年6月27日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Sms.Model
{
    /// <summary>
    /// 消息类型构造参数dbo.Sm_SmsParameter实体类。
    /// </summary>
    [Serializable]
    public class SmsParameter : IModel
    {
        #region 字段

        private int parameterId;
        private int smsTypeId;
        private string parameterType = String.Empty;
        private string paramterValue = String.Empty;
        private Common.StatusEnum parameterStatus;
        private bool isType;
        private string tableName = "dbo.Sm_SmsParameter";

        #endregion

        #region 构造函数

        public SmsParameter()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 参数序号
        /// </summary>
        public int ParameterId
        {
            get { return parameterId; }
            set { parameterId = value; }
        }

        /// <summary>
        /// 消息类型序号
        /// </summary>
        public int SmsTypeId
        {
            get { return smsTypeId; }
            set { smsTypeId = value; }
        }

        /// <summary>
        /// 参数类型
        /// </summary>
        public string ParameterType
        {
            get { return parameterType; }
            set { parameterType = value; }
        }

        /// <summary>
        /// 参数值
        /// </summary>
        public string ParamterValue
        {
            get { return paramterValue; }
            set { paramterValue = value; }
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        public Common.StatusEnum ParameterStatus
        {
            get { return parameterStatus; }
            set { parameterStatus = value; }
        }

        /// <summary>
        /// 是否列表
        /// </summary>
        public bool IsType
        {
            get { return isType; }
            set { isType = value; }
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
            get { return this.parameterId; }
            set { this.parameterId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return parameterStatus; }
            set { parameterStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Sms.DAL.SmsParameterDAL";
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