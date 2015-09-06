
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：test.cs
// 文件功能描述：dbo.test实体类。
// 创建人：CodeSmith
// 创建时间： 2015年4月17日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.Financing.Model
{
    /// <summary>
    /// dbo.test实体类。
    /// </summary>
    [Serializable]
    public class test : IModel
    {
        #region 字段

        private int _id;
        private string _str1 = String.Empty;
        private string _str2 = String.Empty;
        private Common.StatusEnum _status;
        private string tableName = "dbo.test";
        #endregion

        #region 构造函数

        public test()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string str1
        {
            get { return _str1; }
            set { _str1 = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string str2
        {
            get { return _str2; }
            set { _str2 = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Common.StatusEnum status
        {
            get { return _status; }
            set { _status = value; }
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
            get { return this.id; }
            set { this.id = value; }
        }



        /// <summary>
        /// 数据状态
        /// </summary>
        public Common.StatusEnum Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string dalName = "NFMT.Financing.DAL.testDAL";
        public string DalName
        {
            get { return this.dalName; }
            set { this.dalName = value; }
        }

        private string assName = "NFMT.Financing";
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