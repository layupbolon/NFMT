/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SIDetailBLL.cs
// 文件功能描述：价外票明细dbo.Inv_SIDetail业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年8月27日
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
    /// 价外票明细dbo.Inv_SIDetail业务逻辑类。
    /// </summary>
    public class SIDetailBLL : Common.ExecBLL
    {
        private SIDetailDAL sidetailDAL = new SIDetailDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SIDetailDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public SIDetailBLL()
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
            get { return this.sidetailDAL; }
        }
		
        #endregion

        #region 新增方法

        public ResultModel GetSIDetailForUpdate(UserModel user, int sIId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = sidetailDAL.GetSIDetailForUpdate(user, sIId);
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
