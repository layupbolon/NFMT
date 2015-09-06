/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：DataSourceBLL.cs
// 文件功能描述：dbo.DataSource业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年5月21日
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Transactions;
using NFMT.WorkFlow.Model;
using NFMT.WorkFlow.DAL;
using NFMT.WorkFlow.IDAL;
using NFMT.Common;

namespace NFMT.WorkFlow.BLL
{
    /// <summary>
    /// dbo.DataSource业务逻辑类。
    /// </summary>
    public class DataSourceBLL : Common.DataBLL
    {
        private DataSourceDAL datasourceDAL = new DataSourceDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(DataSourceDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataSourceBLL()
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
            get { return this.datasourceDAL; }
        }

        #endregion

        public ResultModel DataSourceComplete(UserModel user, DataSource datasource)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    ResultModel datasourceResult = this.Get(user, datasource.SourceId);
                    datasource = datasourceResult.ReturnValue as DataSource;

                    if (datasource == null)
                    {
                        result.Message = "该数据不存在，不能完成";
                        return result;
                    }

                    if (datasource.Status != StatusEnum.待审核)
                    {
                        result.Message = "非待审核状态的数据不允许完成";
                        return result;
                    }

                    result = datasourceDAL.DataSourceComplete(user, datasource);

                    if (result.ResultStatus == 0)
                        scope.Complete();
                }
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

        /// <summary>
        /// 同步工作流状态（此函数不包含事务）
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="model">工作流对应的业务数据实体</param>
        /// <returns></returns>
        public ResultModel SynchronousStatus(UserModel user, IModel model)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = datasourceDAL.SynchronousStatus(user, model);
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

        public ResultModel BaseAudit(UserModel user, Model.DataSource obj, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.Common.Operate operate = NFMT.Common.Operate.CreateOperate(obj.DalName, obj.AssName);

                if (operate == null)
                {
                    result.Message = "模板不存在";
                    result.ResultStatus = -1;
                    return result;
                }

                result = operate.Get(user, obj.BaseName, obj.TableCode, obj.RowId);

                if (result.ResultStatus == 0)
                {
                    NFMT.Common.IModel model = result.ReturnValue as NFMT.Common.IModel;

                    result = operate.Audit(user, model, isPass);
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
                return result;
            }

            return result;
        }

    }
}
