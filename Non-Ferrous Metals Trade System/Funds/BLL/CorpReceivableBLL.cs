/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：CorpReceivableBLL.cs
// 文件功能描述：收款分配至集团或公司dbo.Fun_CorpReceivable_Ref业务逻辑类。
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
    /// 收款分配至集团或公司dbo.Fun_CorpReceivable_Ref业务逻辑类。
    /// </summary>
    public class CorpReceivableBLL : Common.DataBLL
    {
        private CorpReceivableDAL corpreceivableDAL = new CorpReceivableDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CorpReceivableDAL));
        
		#region 构造函数
        
		/// <summary>
		/// 构造函数
		/// </summary>
		public CorpReceivableBLL()
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
            get { return this.corpreceivableDAL; }
        }
        #endregion

        #region 新增方法

        public ResultModel Load(UserModel user, int allotId, NFMT.Common.StatusEnum status = StatusEnum.已生效)
        {
            return this.corpreceivableDAL.Load(user, allotId, status);
        }

        public ResultModel GetCorpId(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = corpreceivableDAL.GetCorpId(user, allotId);
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

        public ResultModel GetReceIds(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = corpreceivableDAL.GetReceIds(user, allotId);
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

        /// <summary>
        /// 有用，获取当前分配的公司明细
        /// </summary>
        /// <param name="user"></param>
        /// <param name="allotId"></param>
        /// <returns></returns>
        public ResultModel GetRowsDetail(UserModel user, int allotId,NFMT.Common.StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = corpreceivableDAL.GetRowsDetail(user, allotId,status);
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

        public ResultModel GetIsShare(UserModel user, int allotId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = corpreceivableDAL.GetIsShare(user, allotId);
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
