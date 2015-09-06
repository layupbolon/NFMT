/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：InvoiceApplySIDetailBLL.cs
// 文件功能描述：价外票开票申请明细dbo.Inv_InvoiceApplySIDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年7月28日
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
    /// 价外票开票申请明细dbo.Inv_InvoiceApplySIDetail业务逻辑类。
    /// </summary>
    public class InvoiceApplySIDetailBLL : Common.ExecBLL
    {
        private InvoiceApplySIDetailDAL invoiceapplysidetailDAL = new InvoiceApplySIDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(InvoiceApplySIDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InvoiceApplySIDetailBLL()
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
            get { return this.invoiceapplysidetailDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel GetSIIds(UserModel user, int invoiceApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = invoiceapplysidetailDAL.GetSIIDs(user, invoiceApplyId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.Log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.Log.IsInfoEnabled)
                    this.Log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }

        #endregion
    }
}
