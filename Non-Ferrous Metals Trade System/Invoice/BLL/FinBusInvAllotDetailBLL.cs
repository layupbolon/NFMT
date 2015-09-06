/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FinBusInvAllotDetailBLL.cs
// 文件功能描述：业务发票财务发票分配明细dbo.Inv_FinBusInvAllotDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年9月24日
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
    /// 业务发票财务发票分配明细dbo.Inv_FinBusInvAllotDetail业务逻辑类。
    /// </summary>
    public class FinBusInvAllotDetailBLL : Common.ExecBLL
    {
        private FinBusInvAllotDetailDAL finbusinvallotdetailDAL = new FinBusInvAllotDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(FinBusInvAllotDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FinBusInvAllotDetailBLL()
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
            get { return this.finbusinvallotdetailDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel GetJson(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = finbusinvallotdetailDAL.GetJson(user, allotId);
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

        public ResultModel GetBIds(UserModel user, int fId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = finbusinvallotdetailDAL.GetBIds(user, fId);
                if (result.ResultStatus != 0)
                    return result;

                DataTable dt = result.ReturnValue as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    string resultStr = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        resultStr += dr["BusinessInvoiceId"].ToString() + ",";
                    }

                    if (!string.IsNullOrEmpty(resultStr))
                        resultStr = resultStr.Substring(0, resultStr.Length - 1);

                    result.ResultStatus = 0;
                    result.ReturnValue = resultStr;
                    result.Message = "获取成功";
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "获取失败";
                }
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
