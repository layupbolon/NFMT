/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoApplyDetailBLL.cs
// 文件功能描述：回购申请库存明细dbo.RepoApplyDetail业务逻辑类。
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
    /// 回购申请库存明细dbo.RepoApplyDetail业务逻辑类。
    /// </summary>
    public class RepoApplyDetailBLL : Common.ExecBLL
    {
        private RepoApplyDetailDAL repoapplydetailDAL = new RepoApplyDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(RepoApplyDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public RepoApplyDetailBLL()
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
            get { return this.repoapplydetailDAL; }
        }

        #endregion

        #region 新增方法
        public ResultModel GetStockIds(UserModel user, int repoApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = repoapplydetailDAL.GetStockId(user, repoApplyId);
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

        public ResultModel Load(UserModel user, int repoApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = this.repoapplydetailDAL.Load(user,repoApplyId);
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
