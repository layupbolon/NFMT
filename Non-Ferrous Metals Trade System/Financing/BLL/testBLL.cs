/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：testBLL.cs
// 文件功能描述：dbo.test业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年4月17日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Financing.Model;
using NFMT.Financing.DAL;
using NFMT.Financing.IDAL;
using NFMT.Common;

namespace NFMT.Financing.BLL
{
    /// <summary>
    /// dbo.test业务逻辑类。
    /// </summary>
    public class testBLL : Common.ExecBLL
    {
        private testDAL testDAL = new testDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(testDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public testBLL()
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
            get { return this.testDAL; }
        }
		
        #endregion
    }
}
