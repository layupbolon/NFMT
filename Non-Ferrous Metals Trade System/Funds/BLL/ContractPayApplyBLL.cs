/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：ContractPayApplyBLL.cs
// 文件功能描述：合约付款申请dbo.Fun_ContractPayApply_Ref业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
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
    /// 合约付款申请dbo.Fun_ContractPayApply_Ref业务逻辑类。
    /// </summary>
    public class ContractPayApplyBLL : Common.ApplyBLL
    {
        private ContractPayApplyDAL contractpayapplyDAL = new ContractPayApplyDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ContractPayApplyDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContractPayApplyBLL()
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
            get { return this.contractpayapplyDAL; }
        }
        #endregion

        #region 新增方法

        public ResultModel GetByPayApplyId(UserModel user, int payApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.contractpayapplyDAL.GetByPayApplyId(user, payApplyId);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
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
