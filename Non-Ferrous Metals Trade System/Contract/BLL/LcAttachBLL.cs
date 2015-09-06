/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：LcAttachBLL.cs
// 文件功能描述：信用证附件dbo.Con_LcAttach业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月15日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Contract.Model;
using NFMT.Contract.DAL;
using NFMT.Contract.IDAL;
using NFMT.Common;

namespace NFMT.Contract.BLL
{
    /// <summary>
    /// 信用证附件dbo.Con_LcAttach业务逻辑类。
    /// </summary>
    public class LcAttachBLL:Common.ExecBLL
    {
        private LcAttachDAL lcattachDAL = new LcAttachDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(LcAttachDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public LcAttachBLL()
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
            get { return this.lcattachDAL; }
        }
        #endregion
    }
}
