/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：PledgeApplyDetailBLL.cs
// 文件功能描述：质押申请明细dbo.PledgeApplyDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年7月14日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.WareHouse.Model;
using NFMT.WareHouse.DAL;
using NFMT.WareHouse.IDAL;
using NFMT.Common;

namespace NFMT.WareHouse.BLL
{
    /// <summary>
    /// 质押申请明细dbo.PledgeApplyDetail业务逻辑类。
    /// </summary>
    public class PledgeApplyDetailBLL : Common.ExecBLL
    {
        private PledgeApplyDetailDAL pledgeapplydetailDAL = new PledgeApplyDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PledgeApplyDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public PledgeApplyDetailBLL()
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
            get { return this.pledgeapplydetailDAL; }
        }


        public ResultModel GetStockIds(UserModel user, int pledgeApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = pledgeapplydetailDAL.GetStockId(user, pledgeApplyId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，类型序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            return result;
        }
        #endregion
    }
}
