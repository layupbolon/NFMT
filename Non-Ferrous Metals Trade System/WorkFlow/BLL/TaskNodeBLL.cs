/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：TaskNodeBLL.cs
// 文件功能描述：dbo.TaskNode业务逻辑类。
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
using System.Linq;

namespace NFMT.WorkFlow.BLL
{
    /// <summary>
    /// dbo.TaskNode业务逻辑类。
    /// </summary>
    public class TaskNodeBLL : Common.DataBLL
    {
        private TaskNodeDAL tasknodeDAL = new TaskNodeDAL();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(TaskNodeDAL));

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskNodeBLL()
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
            get { return this.tasknodeDAL; }
        }
        #endregion

        #region 新增方法

        public ResultModel GetDetailSelectModel(UserModel user, int taskId, int type, bool hasAttach)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = tasknodeDAL.GetDetailSelectModel(user, taskId, type, hasAttach);
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

        public ResultModel AuditTaskNode(UserModel user, int taskNodeId, bool isPass, string memo, string logResult, string aids)
        {
            ResultModel result = new ResultModel();

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
            {
                NFMT.WorkFlow.DAL.TaskNodeDAL taskNodeDAL = new NFMT.WorkFlow.DAL.TaskNodeDAL();
                result = taskNodeDAL.Get(user, taskNodeId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WorkFlow.Model.TaskNode taskNode = result.ReturnValue as NFMT.WorkFlow.Model.TaskNode;
                if (taskNode == null || taskNode.TaskNodeId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "任务节点不存在";
                    return result;
                }

                if (taskNode.NodeStatus != StatusEnum.待审核)
                {
                    result.ResultStatus = -1;
                    result.Message = "该节点已审核";
                    return result;
                }

                NFMT.WorkFlow.Model.TaskOperateLog taskOperateLog = new NFMT.WorkFlow.Model.TaskOperateLog()
                {
                    TaskNodeId = taskNode.TaskNodeId,
                    EmpId = user.EmpId,
                    Memo = memo,
                    LogTime = DateTime.Now,
                    LogResult = logResult
                };

                List<NFMT.WorkFlow.Model.TaskAttachOperateLog> taskAttachOperateLogs = new List<TaskAttachOperateLog>();
                if (!string.IsNullOrEmpty(aids))
                {
                    foreach (string s in aids.Split(','))
                    {
                        taskAttachOperateLogs.Add(new TaskAttachOperateLog()
                        {
                            AttachId = Convert.ToInt32(s)
                        });
                    }
                }

                FlowOperate flowOperate = new FlowOperate();
                result = flowOperate.AuditTaskNode(user, taskNode, taskOperateLog, taskAttachOperateLogs, isPass);
                if (result.ResultStatus != 0)
                    return result;

                scope.Complete();
            }

            return result;
        }

        public ResultModel GetTaskNodeIdByTaskId(UserModel user, int taskId)
        {
            ResultModel result = new ResultModel();

            try
            {
                result = tasknodeDAL.GetTaskNodeIdByTaskId(user, taskId);
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

        public ResultModel NotifyHandle(UserModel user, Model.TaskOperateLog taskOperateLog)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    result = tasknodeDAL.Get(user, taskOperateLog.TaskNodeId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.TaskNode taskNode = result.ReturnValue as Model.TaskNode;
                    if (taskNode == null || taskNode.TaskNodeId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    if (taskNode.NodeStatus != StatusEnum.待审核)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该节点已审核";
                        return result;
                    }

                    result = tasknodeDAL.Audit(user, taskNode, true);
                    if (result.ResultStatus != 0)
                        return result;

                    DAL.TaskOperateLogDAL taskOperateLogDAL = new TaskOperateLogDAL();
                    result = taskOperateLogDAL.Insert(user, taskOperateLog);
                    if (result.ResultStatus != 0)
                        return result;

                    scope.Complete();
                }
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

        public ResultModel ReturnHandle(UserModel user, Model.TaskOperateLog taskOperateLog)
        {
            ResultModel result = new ResultModel();
            FlowOperate flowOperate = new FlowOperate();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //获取任务节点
                    result = tasknodeDAL.Get(user, taskOperateLog.TaskNodeId);
                    if (result.ResultStatus != 0)
                        return result;

                    Model.TaskNode taskNode = result.ReturnValue as Model.TaskNode;
                    if (taskNode == null || taskNode.TaskNodeId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取失败";
                        return result;
                    }

                    if (taskNode.NodeStatus != StatusEnum.待审核)
                    {
                        result.ResultStatus = -1;
                        result.Message = "该节点已审核";
                        return result;
                    }

                    string cmdText = string.Format("select * from dbo.Wf_TaskNode where TaskId = (select TaskId from dbo.Wf_TaskNode where TaskNodeId = {0}) and NodeStatus <> {1} ", taskOperateLog.TaskNodeId, (int)Common.StatusEnum.已作废);
                    result = tasknodeDAL.Load<Model.TaskNode>(user, CommandType.Text, cmdText);
                    if (result.ResultStatus != 0)
                        return result;

                    List<Model.TaskNode> taskNodes = result.ReturnValue as List<Model.TaskNode>;
                    if (taskNodes == null || !taskNodes.Any())
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取任务节点失败";
                        return result;
                    }

                    //如果是第一层节点就直接拒绝
                    if (taskNodes.Select(a => a.NodeLevel).Where(a => a > 0).Distinct().Count() == 1)
                    {
                        result = flowOperate.AuditTaskNode(user, taskNode, taskOperateLog, null, false);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                    else
                    {
                        //将当前层级上的所有审核人都视为审核拒绝
                        result = tasknodeDAL.UpdateTaskNodeStatusByLevelId(user, taskNode.TaskId, taskNode.NodeLevel, Common.StatusEnum.审核拒绝);
                        if (result.ResultStatus != 0)
                            return result;

                        //写入审核操作记录
                        DAL.TaskOperateLogDAL taskOperateLogDAL = new TaskOperateLogDAL();
                        result = taskOperateLogDAL.Insert(user, taskOperateLog);
                        if (result.ResultStatus != 0)
                            return result;

                        int lastNodeLevel = taskNodes.Where(a => a.NodeLevel < taskNode.NodeLevel).Select(a => a.NodeLevel).Max();

                        //修改上一层节点的状态
                        result = tasknodeDAL.UpdateTaskNodeStatusByLevelId(user, taskNode.TaskId, lastNodeLevel, StatusEnum.已作废);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取数据源
                        DAL.DataSourceDAL dataSourceDAL = new DataSourceDAL();
                        result = dataSourceDAL.GetDataSourceByTaskNodeId(user, taskNode.TaskNodeId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.DataSource dataSource = result.ReturnValue as Model.DataSource;

                        //获取任务
                        DAL.TaskDAL taskDAL = new TaskDAL();
                        result = taskDAL.Get(user, taskNode.TaskId);
                        if (result.ResultStatus != 0)
                            return result;

                        Model.Task task = result.ReturnValue as Model.Task;

                        result = flowOperate.CreateTaskNodes(user, task, lastNodeLevel, dataSource);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    scope.Complete();
                }
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
