
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：Bloc.cs
// 文件功能描述：dbo.Bloc实体类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
    /// <summary>
    /// dbo.Bloc实体类。
    /// </summary>
    [Serializable]
    public class Bloc : IModel
    {
        #region 字段

        private int blocId;
        private string blocName = String.Empty;
        private string blocFullName = String.Empty;
        private string blocEname = String.Empty;
        private Common.StatusEnum blocStatus;
        private string tableName = "dbo.Bloc";

        #endregion

        #region 构造函数

        public Bloc()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int BlocId
        {
            get { return blocId; }
            set { blocId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BlocName
        {
            get { return blocName; }
            set { blocName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BlocFullName
        {
            get { return blocFullName; }
            set { blocFullName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BlocEname
        {
            get { return blocEname; }
            set { blocEname = value; }
        }

        public bool IsSelf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum BlocStatus
        {
            get { return blocStatus; }
            set { blocStatus = value; }
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
            get { return this.blocId; }
            set { this.blocId = value; }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return blocStatus; }
            set { blocStatus = value; }
        }
        /// <summary>
        /// 数据状态名
        /// </summary>
        public string BlocStatusName
        {
            get { return this.blocStatus.ToString(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.User.DAL.BlocDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.User";
        public string AssName
        {
            get { return this.assName; }
            set { this.assName = value; }
        }

        public string StatusName
        {
            get { return this.Status.ToString(); }
        }

        private string dataBaseName = "NFMT_User";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}