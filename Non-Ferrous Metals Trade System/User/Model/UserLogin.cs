
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：UserLogin.cs
// 文件功能描述：用户登录dbo.UserLogin实体类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using NFMT.Common;

namespace NFMT.User.Model
{
    /// <summary>
    /// 用户登录dbo.UserLogin实体类。
    /// </summary>
    [Serializable]
    public class UserLogin : IModel
    {
        #region 字段

        private int userLoginId;
        private int accId;
        private DateTime loginTime;
        private string loginIP = String.Empty;
        private string loginMac = String.Empty;
        private int borwser;
        private string tableName = "dbo.UserLogin";

        #endregion

        #region 构造函数

        public UserLogin()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public int UserLoginId
        {
            get { return userLoginId; }
            set { userLoginId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AccId
        {
            get { return accId; }
            set { accId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LoginTime
        {
            get { return loginTime; }
            set { loginTime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LoginIP
        {
            get { return loginIP; }
            set { loginIP = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LoginMac
        {
            get { return loginMac; }
            set { loginMac = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Borwser
        {
            get { return borwser; }
            set { borwser = value; }
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
            get { return this.userLoginId; }
            set { this.userLoginId = value; }
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

        public string DalName { get; set; }

        public string AssName { get; set; }

        private string dataBaseName = "NFMT_User";
        public string DataBaseName
        {
            get { return this.dataBaseName; }
        }
        #endregion
    }
}