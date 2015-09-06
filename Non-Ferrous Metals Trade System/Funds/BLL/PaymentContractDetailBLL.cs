/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PaymentContractDetailBLL.cs
// 文件功能描述：合约财务付款明细dbo.Fun_PaymentContractDetail业务逻辑类。
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
    /// 合约财务付款明细dbo.Fun_PaymentContractDetail业务逻辑类。
    /// </summary>
    public class PaymentContractDetailBLL : Common.ExecBLL
    {
        private PaymentContractDetailDAL paymentcontractdetailDAL = new PaymentContractDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PaymentContractDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public PaymentContractDetailBLL()
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
            get { return this.paymentcontractdetailDAL; }
        }
		
        #endregion

        #region 新增方法

        public ResultModel GetByPaymentId(UserModel user, int paymentId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = paymentcontractdetailDAL.GetByPaymentId(user, paymentId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
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
