/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PricingPersonBLL.cs
// 文件功能描述：点价权限人dbo.Pri_PricingPerson业务逻辑类。
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
    /// 点价权限人dbo.Pri_PricingPerson业务逻辑类。
    /// </summary>
    public class PricingPersonBLL : Common.ExecBLL
    {
        private PricingPersonDAL pricingpersonDAL = new PricingPersonDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PricingPersonDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public PricingPersonBLL()
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
            get { return this.pricingpersonDAL; }
        }
		
        #endregion
    }
}
