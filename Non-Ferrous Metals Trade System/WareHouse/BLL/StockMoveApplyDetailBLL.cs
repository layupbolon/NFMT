/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMoveApplyDetailBLL.cs
// 文件功能描述：回购申请库存明细dbo.StockMoveApplyDetail业务逻辑类。
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
    /// 回购申请库存明细dbo.StockMoveApplyDetail业务逻辑类。
    /// </summary>
    public class StockMoveApplyDetailBLL : Common.ExecBLL
    {
        private StockMoveApplyDetailDAL stockmoveapplydetailDAL = new StockMoveApplyDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockMoveApplyDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockMoveApplyDetailBLL()
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
            get { return this.stockmoveapplydetailDAL; }
        }

        #endregion

        public ResultModel GetStockIds(UserModel user, int stockMoveApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = stockmoveapplydetailDAL.GetStockId(user, stockMoveApplyId);
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
       
    }
}
