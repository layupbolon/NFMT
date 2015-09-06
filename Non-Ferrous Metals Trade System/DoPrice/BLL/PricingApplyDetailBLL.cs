/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingApplyDetailBLL.cs
// 文件功能描述：点价申请明细dbo.Pri_PricingApplyDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年9月2日
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
    /// 点价申请明细dbo.Pri_PricingApplyDetail业务逻辑类。
    /// </summary>
    public class PricingApplyDetailBLL : Common.ApplyBLL
    {
        private PricingApplyDetailDAL pricingapplydetailDAL = new PricingApplyDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PricingApplyDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public PricingApplyDetailBLL()
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
            get { return this.pricingapplydetailDAL; }
        }
		
        #endregion
    }
}
