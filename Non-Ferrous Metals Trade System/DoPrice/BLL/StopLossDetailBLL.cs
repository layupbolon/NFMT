/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StopLossDetailBLL.cs
// 文件功能描述：止损明细表dbo.Pri_StopLossDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年10月23日
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
    /// 止损明细表dbo.Pri_StopLossDetail业务逻辑类。
    /// </summary>
    public class StopLossDetailBLL : Common.ExecBLL
    {
        private StopLossDetailDAL stoplossdetailDAL = new StopLossDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StopLossDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StopLossDetailBLL()
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
            get { return this.stoplossdetailDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel GetCanStopLossApplyDetailIds(UserModel user, int stopLossId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = stoplossdetailDAL.GetCanStopLossApplyDetailIds(user, stopLossId);
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

        public ResultModel GetStopLossApplyDetailIds(UserModel user, int pricingId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = stoplossdetailDAL.GetStopLossApplyDetailIds(user, pricingId);
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
