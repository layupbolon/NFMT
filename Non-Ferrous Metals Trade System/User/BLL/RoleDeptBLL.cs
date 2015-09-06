/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RoleDeptBLL.cs
// 文件功能描述：角色部门关联表dbo.RoleDept业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年11月12日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.User.Model;
using NFMT.User.DAL;
using NFMT.User.IDAL;
using NFMT.Common;

namespace NFMT.User.BLL
{
    /// <summary>
    /// 角色部门关联表dbo.RoleDept业务逻辑类。
    /// </summary>
    public class RoleDeptBLL : Common.ExecBLL
    {
        private RoleDeptDAL roledeptDAL = new RoleDeptDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(RoleDeptDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public RoleDeptBLL()
		{
		}
        
		#endregion

        #region 数据库操作
		
        protected override log4net.ILog Log
        {
            get { return this.log; }
        }

        public override IOperate Operate
        {
            get { return this.roledeptDAL; }
        }
		
        #endregion
    }
}
