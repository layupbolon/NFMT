/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：LcReceivableBLL.cs
// 文件功能描述：信用证收款登记dbo.Fun_LcReceivable_Ref业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Funds.Model;
using NFMT.Funds.DAL;
using NFMT.Funds.IDAL;
using NFMT.Common;

namespace NFMT.Funds.BLL
{
    /// <summary>
    /// 信用证收款登记dbo.Fun_LcReceivable_Ref业务逻辑类。
    /// </summary>
    public class LcReceivableBLL : Common.ExecBLL
    {
        private LcReceivableDAL lcreceivableDAL = new LcReceivableDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(LcReceivableDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public LcReceivableBLL()
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
            get { return this.lcreceivableDAL; }
        }
        #endregion
    }
}
