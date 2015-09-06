/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InterestDetailBLL.cs
// 文件功能描述：利息明细dbo.Pri_InterestDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年3月12日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.DoPrice.Model;
using NFMT.DoPrice.DAL;
using NFMT.DoPrice.IDAL;
using NFMT.Common;

namespace NFMT.DoPrice.BLL
{
    /// <summary>
    /// 利息明细dbo.Pri_InterestDetail业务逻辑类。
    /// </summary>
    public class InterestDetailBLL : Common.ExecBLL
    {
        private InterestDetailDAL interestdetailDAL = new InterestDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(InterestDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public InterestDetailBLL()
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
            get { return this.interestdetailDAL; }
        }
		
        #endregion
    }
}
