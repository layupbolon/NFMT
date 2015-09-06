/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FlowMasterConfigBLL.cs
// 文件功能描述：流程模版配置表dbo.Wf_FlowMasterConfig业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年4月23日
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
    /// 流程模版配置表dbo.Wf_FlowMasterConfig业务逻辑类。
    /// </summary>
    public class FlowMasterConfigBLL : Common.ExecBLL
    {
        private FlowMasterConfigDAL flowmasterconfigDAL = new FlowMasterConfigDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(FlowMasterConfigDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public FlowMasterConfigBLL()
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
            get { return this.flowmasterconfigDAL; }
        }
		
        #endregion
    }
}
