/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StopLossApplyDetailBLL.cs
// 文件功能描述：止损申请明细dbo.Pri_StopLossApplyDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年10月23日
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
    /// 止损申请明细dbo.Pri_StopLossApplyDetail业务逻辑类。
    /// </summary>
    public class StopLossApplyDetailBLL : Common.ExecBLL
    {
        private StopLossApplyDetailDAL stoplossapplydetailDAL = new StopLossApplyDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StopLossApplyDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public StopLossApplyDetailBLL()
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
            get { return this.stoplossapplydetailDAL; }
        }
		
        #endregion
    }
}
