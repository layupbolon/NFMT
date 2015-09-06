/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：TaskAttachBLL.cs
// 文件功能描述：任务附件dbo.Wf_TaskAttach业务逻辑类。
// 创建人：CodeSmith
// 创建时间： 2014年12月31日
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
    /// 任务附件dbo.Wf_TaskAttach业务逻辑类。
    /// </summary>
    public class TaskAttachBLL : Common.ExecBLL
    {
        private TaskAttachDAL taskattachDAL = new TaskAttachDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(TaskAttachDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskAttachBLL()
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
            get { return this.taskattachDAL; }
        }

        #endregion

        #region 新增方法

        public ResultModel InsertAttach(UserModel user, int taskId, string aids)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (!string.IsNullOrEmpty(aids))
                {
                    foreach (string i in aids.Split(','))
                    {
                        result = taskattachDAL.Insert(user, new Model.TaskAttach()
                        {
                            TaskId = taskId,
                            AttachId = Convert.ToInt32(i)
                        });

                        if (result.ResultStatus != 0)
                            return result;
                    }
                }
                result.ResultStatus = 0;

            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        public ResultModel GetTaskAttachByTaskId(UserModel user, int taskId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = taskattachDAL.GetTaskAttachByTaskId(user, taskId);
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
