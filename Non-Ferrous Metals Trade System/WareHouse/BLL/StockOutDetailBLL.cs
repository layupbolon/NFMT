/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：StockOutDetailBLL.cs
// 文件功能描述：出库明细dbo.St_StockOutDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月5日
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
    /// 出库明细dbo.St_StockOutDetail业务逻辑类。
    /// </summary>
    public class StockOutDetailBLL : Common.ExecBLL
    {
        private StockOutDetailDAL stockoutdetailDAL = new StockOutDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(StockOutDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public StockOutDetailBLL()
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
            get { return this.stockoutdetailDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user,int stockOutId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = stockoutdetailDAL.Load(user, stockOutId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (log.IsInfoEnabled)
                    log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }
            return result;
        }
        #endregion
    }
}
