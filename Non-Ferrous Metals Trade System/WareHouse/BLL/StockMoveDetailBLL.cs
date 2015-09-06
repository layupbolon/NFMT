/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockMoveDetailBLL.cs
// 文件功能描述：移库明细dbo.StockMoveDetail业务逻辑类。
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
    /// 移库明细dbo.StockMoveDetail业务逻辑类。
    /// </summary>
    public class StockMoveDetailBLL : Common.ExecBLL
    {
        private StockMoveDetailDAL stockmovedetailDAL = new StockMoveDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockMoveDetailDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StockMoveDetailBLL()
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
            get { return this.stockmovedetailDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int stockMoveId, Common.StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = stockmovedetailDAL.Load(user, stockMoveId, status);
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
