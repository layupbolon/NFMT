
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractMaster.cs
// 文件功能描述：合约模板dbo.ContractMaster实体类。
// 创建人：CodeSmith
// 创建时间： 2014年7月8日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
    /// <summary>
    /// 合约模板dbo.ContractMaster实体类。
    /// </summary>
    [Serializable]
    public class ContractMaster : IModel
    {
        #region 字段

        private int masterId;
        private string masterName = String.Empty;
        private string masterEname = String.Empty;
        private Common.StatusEnum masterStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.ContractMaster";

        #endregion

        #region 构造函数

        public ContractMaster()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 模板序号
        /// </summary>
        public int MasterId
        {
            get { return masterId; }
            set { masterId = value; }
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string MasterName
        {
            get { return masterName; }
            set { masterName = value; }
        }

        /// <summary>
        /// 模板英文名称
        /// </summary>
        public string MasterEname
        {
            get { return masterEname; }
            set { masterEname = value; }
        }

        /// <summary>
        /// 模板状态
        /// </summary>
        public Common.StatusEnum MasterStatus
        {
            get { return masterStatus; }
            set { masterStatus = value; }
        }

        /// <summary>
        /// 模板状态名称
        /// </summary>
        public string MasterStatusName
        {
            get { return this.MasterStatus.ToString(); }
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
            get { return this.masterId; }
            set { this.masterId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return masterStatus; }
            set { masterStatus = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.ContractMasterDAL";
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