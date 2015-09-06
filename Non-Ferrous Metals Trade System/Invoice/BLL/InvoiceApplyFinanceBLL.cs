/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceApplyFinanceBLL.cs
// 文件功能描述：开票申请与财务票关联表dbo.Inv_InvoiceApplyFinance业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年7月21日
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
    /// 开票申请与财务票关联表dbo.Inv_InvoiceApplyFinance业务逻辑类。
    /// </summary>
    public class InvoiceApplyFinanceBLL : Common.ExecBLL
    {
        private InvoiceApplyFinanceDAL invoiceapplyfinanceDAL = new InvoiceApplyFinanceDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(InvoiceApplyFinanceDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InvoiceApplyFinanceBLL()
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
            get { return this.invoiceapplyfinanceDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel GetInvApplyIdByFinInvId(UserModel user, int financeInvoiceId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = invoiceapplyfinanceDAL.GetInvApplyIdByFinInvId(user, financeInvoiceId);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion
    }
}
