/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：TaskOperateLogBLL.cs
// 文件功能描述：dbo.TaskOperateLog业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.WorkFlow.Model;
using NFMT.WorkFlow.DAL;
using NFMT.WorkFlow.IDAL;
using NFMT.Common;

namespace NFMT.WorkFlow.BLL
{
    /// <summary>
    /// dbo.TaskOperateLog业务逻辑类。
    /// </summary>
    public class TaskOperateLogBLL : Common.DataBLL
    {
        private TaskOperateLogDAL taskoperatelogDAL = new TaskOperateLogDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(TaskOperateLogDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public TaskOperateLogBLL()
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
            get { return this.taskoperatelogDAL; }
        }
        
        #endregion
    }
}
