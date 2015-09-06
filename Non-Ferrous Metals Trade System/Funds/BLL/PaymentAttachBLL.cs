/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentAttachBLL.cs
// 文件功能描述：财务付款附件dbo.Fun_PaymentAttach业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月15日
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
    /// 财务付款附件dbo.Fun_PaymentAttach业务逻辑类。
    /// </summary>
    public class PaymentAttachBLL : Common.ExecBLL
    {
        private PaymentAttachDAL paymentattachDAL = new PaymentAttachDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PaymentAttachDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public PaymentAttachBLL()
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
            get { return this.paymentattachDAL; }
        }
		
        #endregion
    }
}
