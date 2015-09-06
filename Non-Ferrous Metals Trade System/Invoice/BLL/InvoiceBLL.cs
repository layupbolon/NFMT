/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceBLL.cs
// 文件功能描述：发票dbo.Invoice业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年9月1日
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
    /// 发票dbo.Invoice业务逻辑类。
    /// </summary>
    public class InvoiceBLL : Common.ExecBLL
    {
        private InvoiceDAL invoiceDAL = new InvoiceDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(InvoiceDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public InvoiceBLL()
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
            get { return this.invoiceDAL; }
        }
		
        #endregion
    }
}
