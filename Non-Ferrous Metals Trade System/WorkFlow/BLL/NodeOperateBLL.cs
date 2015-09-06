/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：NodeOperateBLL.cs
// 文件功能描述：节点操作表dbo.Wf_NodeOperate业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年4月17日
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
    /// 节点操作表dbo.Wf_NodeOperate业务逻辑类。
    /// </summary>
    public class NodeOperateBLL : Common.ExecBLL
    {
        private NodeOperateDAL nodeoperateDAL = new NodeOperateDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(NodeOperateDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public NodeOperateBLL()
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
            get { return this.nodeoperateDAL; }
        }
		
        #endregion
    }
}
