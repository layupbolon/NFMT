
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Area.cs
// 文件功能描述：地区表dbo.Area实体类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Data.Model
{
    /// <summary>
    /// 地区表dbo.Area实体类。
    /// </summary>
    [Serializable]
    public class Area : IModel
    {
        #region 字段

        private int areaId;
        private string areaName = String.Empty;
        private string areaFullName = String.Empty;
        private string areaShort = String.Empty;
        private string areaCode = String.Empty;
        private string areaZip = String.Empty;
        private int areaLevel;
        private int parentID;
        private Common.StatusEnum areaStatus;
        private int creatorId;
        private DateTime createTime;
        private int lastModifyId;
        private DateTime lastModifyTime;
        private string tableName = "dbo.Area";

        #endregion

        #region 构造函数

        public Area()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int AreaId
        {
            get { return areaId; }
            set { areaId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AreaFullName
        {
            get { return areaFullName; }
            set { areaFullName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AreaShort
        {
            get { return areaShort; }
            set { areaShort = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AreaCode
        {
            get { return areaCode; }
            set { areaCode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AreaZip
        {
            get { return areaZip; }
            set { areaZip = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AreaLevel
        {
            get { return areaLevel; }
            set { areaLevel = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }
        /// <summary>
        /// 上级区域名称
        /// </summary>
        public string atAreaName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum AreaStatus
        {
            get { return areaStatus; }
            set { areaStatus = value; }
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
            get { return this.areaId; }
            set { this.areaId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return areaStatus; }
            set { areaStatus = value; }
        }
        /// <summary>
        /// 数据状态名
        /// </summary>
        public string AreaStatusName
        {
            get { return this.areaStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Data.DAL.AreaDAL";
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