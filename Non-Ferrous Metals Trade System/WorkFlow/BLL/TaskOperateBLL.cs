/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：TaskOperateBLL.cs
// 文件功能描述：任务操作表dbo.Wf_TaskOperate业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2015年4月17日
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
    /// 任务操作表dbo.Wf_TaskOperate业务逻辑类。
    /// </summary>
    public class TaskOperateBLL : Common.ExecBLL
    {
        private TaskOperateDAL taskoperateDAL = new TaskOperateDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(TaskOperateDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskOperateBLL()
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
            get { return this.taskoperateDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel GetByTaskNodeId(UserModel user, int taskNodeId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = taskoperateDAL.GetByTaskNodeId(user, taskNodeId);
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
