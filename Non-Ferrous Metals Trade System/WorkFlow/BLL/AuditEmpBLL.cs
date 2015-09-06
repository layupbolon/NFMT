/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：AuditEmpBLL.cs
// 文件功能描述：审核人表dbo.Wf_AuditEmp业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年11月12日
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
    /// 审核人表dbo.Wf_AuditEmp业务逻辑类。
    /// </summary>
    public class AuditEmpBLL : Common.ExecBLL
    {
        private AuditEmpDAL auditempDAL = new AuditEmpDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(AuditEmpDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public AuditEmpBLL()
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
            get { return this.auditempDAL; }
        }
		
        #endregion
    }
}
