/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：NodeBLL.cs
// 文件功能描述：节点表dbo.Wf_Node业务逻辑类。
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
    /// 节点表dbo.Wf_Node业务逻辑类。
    /// </summary>
    public class NodeBLL : Common.ExecBLL
    {
        private NodeDAL nodeDAL = new NodeDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(NodeDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public NodeBLL()
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
            get { return this.nodeDAL; }
        }
		
        #endregion
    }
}
