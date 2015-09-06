/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CashInInvoiceBLL.cs
// 文件功能描述：收款分配至发票dbo.Fun_CashInInvoice_Ref业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年7月29日
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
    /// 收款分配至发票dbo.Fun_CashInInvoice_Ref业务逻辑类。
    /// </summary>
    public class CashInInvoiceBLL : Common.ExecBLL
    {
        private CashInInvoiceDAL cashininvoiceDAL = new CashInInvoiceDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CashInInvoiceDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CashInInvoiceBLL()
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
            get { return this.cashininvoiceDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel GetSIIdsbyAllotId(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = cashininvoiceDAL.GetSIIdsbyAllotId(user, allotId);
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        #endregion
    }
}
