/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：UserModel.cs
// 文件功能描述：当前用户实体。
// 创建人：pekah.chow
// 创建时间： 2014-04-23
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace NFMT.Common
{
    public class UserModel
    {
        private int accountId = 0;
        private string accountName = string.Empty;        
        private int empId = 0;        
        private string empName = string.Empty;
        private int corpId = 0;
        private int deptId = 0;
        private int blocId = 0;
        private string cookieValue = string.Empty;

        /// <summary>
        /// 账户序号
        /// </summary>
        public int AccountId 
        {
            get { return this.accountId; }
            set { this.accountId = value; }
        }

        /// <summary>
        /// 账户名
        /// </summary>
        public string AccountName
        { 
            get { return this.accountName; }
            set { this.accountName = value; }
        }

        /// <summary>
        /// 所属员工序号
        /// </summary>
        public int EmpId 
        {
            get { return this.empId; }
            set { this.empId = value; }
        }

        /// <summary>
        /// 员工名称
        /// </summary>
        public string EmpName 
        {
            get { return this.empName; }
            set { this.empName = value; }
        }

        /// <summary>
        /// 集团序号
        /// </summary>
        public int BlocId
        {
            get { return this.blocId; }
            set { this.blocId = value; }
        }

        /// <summary>
        /// 所属公司
        /// </summary>
        public int CorpId
        {
            get { return this.corpId; }
            set { this.corpId = value; }
        }

        /// <summary>
        /// 所属部门
        /// </summary>
        public int DeptId
        {
            get { return this.deptId; }
            set { this.deptId = value; }
        }

        public string CookieValue
        {
            get { return this.cookieValue; }
            set { this.cookieValue = value; }
        }
    }
}
