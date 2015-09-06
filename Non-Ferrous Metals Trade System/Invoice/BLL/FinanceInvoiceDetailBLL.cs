/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FinanceInvoiceDetailBLL.cs
// 文件功能描述：财务发票明细dbo.Inv_FinanceInvoiceDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月25日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.Invoice.Model;
using NFMT.Invoice.DAL;
using NFMT.Invoice.IDAL;
using NFMT.Common;

namespace NFMT.Invoice.BLL
{
    /// <summary>
    /// 财务发票明细dbo.Inv_FinanceInvoiceDetail业务逻辑类。
    /// </summary>
    public class FinanceInvoiceDetailBLL : Common.ExecBLL
    {
        private FinanceInvoiceDetailDAL financeinvoicedetailDAL = new FinanceInvoiceDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(FinanceInvoiceDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public FinanceInvoiceDetailBLL()
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
            get { return this.financeinvoicedetailDAL; }
        }
		
        #endregion
    }
}
