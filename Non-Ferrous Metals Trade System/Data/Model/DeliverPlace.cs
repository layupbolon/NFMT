
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DeliverPlace.cs
// 文件功能描述：交货地dbo.DeliverPlace实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月16日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
    /// <summary>
    /// 交货地dbo.DeliverPlace实体类。
    /// </summary>
    [Serializable]
    public class DeliverPlace : IModel
    {
        #region 字段

        private int dPId;
        private int dPType;
        private int dPArea;
        private int dPCompany;
        private string dPName = String.Empty;
        private string dPFullName = String.Empty;
        private Common.StatusEnum dPStatus;
        private string dPAddress = String.Empty;
        private string dPEAddress = String.Empty;
        private string dPTel = String.Empty;
        private string dPContact = String.Empty;
        private string dPFax = String.Empty;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.DeliverPlace";

        #endregion

        #region 构造函数

        public DeliverPlace()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int DPId
        {
            get { return dPId; }
            set { dPId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DPType
        {
            get { return dPType; }
            set { dPType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DPArea
        {
            get { return dPArea; }
            set { dPArea = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DPCompany
        {
            get { return dPCompany; }
            set { dPCompany = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DPName
        {
            get { return dPName; }
            set { dPName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DPFullName
        {
            get { return dPFullName; }
            set { dPFullName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum DPStatus
        {
            get { return dPStatus; }
            set { dPStatus = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DPAddress
        {
            get { return dPAddress; }
            set { dPAddress = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DPEAddress
        {
            get { return dPEAddress; }
            set { dPEAddress = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DPTel
        {
            get { return dPTel; }
            set { dPTel = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DPContact
        {
            get { return dPContact; }
            set { dPContact = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DPFax
        {
            get { return dPFax; }
            set { dPFax = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CreatorId
        {
            get { return creatorId; }
            set { creatorId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int LastModifyId
        {
            get { return lastModifyId; }
            set { lastModifyId = value; }
        }

        /// <summary>
        /// 
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
            get { return this.dPId; }
            set { this.dPId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return dPStatus; }
            set { dPStatus = value; }
        }

        /// <summary>
        /// 数据状态名
        /// </summary>
        public string DPStatusName
        {
            get { return this.dPStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.DeliverPlaceDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Data";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT_Basic";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}