/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：EmpMenuBLL.cs
// 文件功能描述：员工菜单关系表dbo.EmpMenu业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年11月20日
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
    /// 员工菜单关系表dbo.EmpMenu业务逻辑类。
    /// </summary>
    public class EmpMenuBLL : Common.ExecBLL
    {
        private EmpMenuDAL empmenuDAL = new EmpMenuDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(EmpMenuDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public EmpMenuBLL()
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
            get { return this.empmenuDAL; }
        }
		
        #endregion
    }
}
