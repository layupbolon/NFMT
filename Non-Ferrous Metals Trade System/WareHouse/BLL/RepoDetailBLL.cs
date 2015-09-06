/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：RepoDetailBLL.cs
// 文件功能描述：回购明细dbo.RepoDetail业务逻辑类。
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
    /// 回购明细dbo.RepoDetail业务逻辑类。
    /// </summary>
    public class RepoDetailBLL : Common.ExecBLL
    {
        private RepoDetailDAL repodetailDAL = new RepoDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(RepoDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public RepoDetailBLL()
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
            get { return this.repodetailDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel GetStockIds(UserModel user, int repoId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = repodetailDAL.GetStockId(user, repoId);
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
